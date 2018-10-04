namespace TogglerService.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using TogglerService.Models;

    public class GlobalToggleRepository : IGlobalToggleRepository
    {
        private static readonly List<GlobalToggle> Toggles;

        static GlobalToggleRepository() =>
            Toggles = new List<GlobalToggle>()
            {
                new GlobalToggle()
                {
                    Id = "isButtonBlue",
                    Value = true,
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                },
                new GlobalToggle()
                {
                    Id = "isButtonRed",
                    Value = false,
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                }
            };

        public Task<GlobalToggle> Add(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            Toggles.Add(toggle);
            toggle.Id = Toggles.Max(x => x.Id) + 1;
            return Task.FromResult(toggle);
        }

        public Task Delete(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            if (Toggles.Contains(toggle))
            {
                Toggles.Remove(toggle);
            }

            return Task.CompletedTask;
        }

        public Task<GlobalToggle> Get(string toggleId, CancellationToken cancellationToken)
        {
            var toggle = Toggles.FirstOrDefault(x => x.Id == toggleId);
            return Task.FromResult(toggle);
        }

        public Task<ICollection<GlobalToggle>> GetAll( CancellationToken cancellationToken)
        {
            return Task.FromResult((ICollection<GlobalToggle>)Toggles);
        }


        public Task<GlobalToggle> Update(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            var existingToggle = Toggles.FirstOrDefault(x => x.Id == toggle.Id);
            existingToggle.Value = toggle.Value;
            return Task.FromResult(toggle);
        }
    }
}
