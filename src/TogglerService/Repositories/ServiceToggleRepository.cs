using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;

namespace TogglerService.Repositories
{


    public class ServiceToggleRepository : IServiceToggleRepository
    {
        private static readonly List<ServiceToggle> Toggles;

#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ServiceToggleRepository()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            Toggles = new List<ServiceToggle>()
            {
                new ServiceToggle()
                {
                    Id = "isButtonBlue",
                    ServiceId = "ABC",
                    Value = false,
                    VersionRange = "*",
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                },
                new ServiceToggle()
                {
                    Id = "isButtonGreen",
                    ServiceId = "ABC",
                    Value = true,
                    VersionRange = "*",
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                },
            };
        }

        public Task<ServiceToggle> Add(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            Toggles.Add(toggle);
            toggle.Id = Toggles.Max(x => x.Id) + 1;
            return Task.FromResult(toggle);
        }

        public Task Delete(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            if (Toggles.Contains(toggle))
            {
                Toggles.Remove(toggle);
            }

            return Task.CompletedTask;
        }

        public Task<ServiceToggle> GetById(string toggleId, CancellationToken cancellationToken)
        {
            ServiceToggle toggle = Toggles.FirstOrDefault(x => x.Id == toggleId);
            return Task.FromResult(toggle);
        }

        public Task<ICollection<ServiceToggle>> GetAll(CancellationToken cancellationToken)
        {
            return Task.FromResult((ICollection<ServiceToggle>)Toggles);
        }

        public Task<ServiceToggle> Update(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            ServiceToggle existingToggle = Toggles.FirstOrDefault(x => x.Id == toggle.Id && x.ServiceId == toggle.ServiceId);
            existingToggle.Value = toggle.Value;
            existingToggle.VersionRange = toggle.VersionRange;
            existingToggle.Modified = DateTime.UtcNow;
            return Task.FromResult(toggle);
        }
    }
}
