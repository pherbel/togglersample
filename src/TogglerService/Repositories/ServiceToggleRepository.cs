using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Data;
using TogglerService.Models;
using TogglerService.Services;

namespace TogglerService.Repositories
{


    public class ServiceToggleRepository : EFRepositoryBase, IServiceToggleRepository
    {
        private readonly IClockService _clockService;

        public ServiceToggleRepository(ApplicationDbContext context, IClockService clockService) : base(context)
        {
            if (clockService is null)
                throw new ArgumentNullException(nameof(clockService));

            _clockService = clockService;
        }

        public async Task<ServiceToggle> Add(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            DateTimeOffset now = _clockService.UtcNow;
            toggle.Created = now;
            toggle.Modified = now;
            Context.ServiceToggles.Add(toggle);
            await Save();
            return toggle;
        }

        public async Task Delete(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            if (await Context.GlobalToggles.AnyAsync(t => t.Id == t.Id, cancellationToken))
            {
                Context.ServiceToggles.Remove(toggle);
                await Save(cancellationToken);
            }
        }

        public Task<ServiceToggle> GetById(string toggleId, string serviceId, CancellationToken cancellationToken)
        {
            return Context.ServiceToggles.SingleOrDefaultAsync(t => t.Id == toggleId && t.ServiceId == serviceId, cancellationToken);

        }

        public Task<List<ServiceToggle>> GetAll(CancellationToken cancellationToken)
        {
            return Context.ServiceToggles.ToListAsync(cancellationToken);
        }

        public Task<List<ServiceToggle>> GetAllByToggleId(string toggleId, CancellationToken cancellationToken)
        {
            return Context.ServiceToggles.Where(t=> t.Id == toggleId).ToListAsync(cancellationToken);
        }

        public Task<List<ServiceToggle>> GetAllByServiceId(string serviceId, CancellationToken cancellationToken)
        {
            return Context.ServiceToggles.Where(t => t.ServiceId == serviceId).ToListAsync(cancellationToken);
        }

        public async Task<ServiceToggle> Update(ServiceToggle toggle, CancellationToken cancellationToken)
        {
            ServiceToggle existingToggle = await Context.ServiceToggles.SingleOrDefaultAsync(t => t.Id == toggle.Id, cancellationToken);
            existingToggle.Value = toggle.Value;
            existingToggle.VersionRange = toggle.VersionRange;
            existingToggle.Modified = _clockService.UtcNow;
            Context.Update(existingToggle);
            await Save(cancellationToken);

            return existingToggle;
        }
    }
}
