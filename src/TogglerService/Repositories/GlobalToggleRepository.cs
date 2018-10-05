using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Data;
using TogglerService.Models;
using TogglerService.Services;

namespace TogglerService.Repositories
{


    public class GlobalToggleRepository : EFRepositoryBase, IGlobalToggleRepository
    {
        private readonly IClockService _clockService;

        public GlobalToggleRepository(ApplicationDbContext context, IClockService clockService) : base(context)
        {
            if (clockService is null)
            {
                throw new ArgumentNullException(nameof(clockService));
            }

            _clockService = clockService;

        }

        public async Task<GlobalToggle> Add(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            DateTimeOffset now = _clockService.UtcNow;
            toggle.Created = now;
            toggle.Modified = now;
            await Context.GlobalToggles.AddAsync(toggle);
            await Context.ExcludedServices.AddRangeAsync(toggle.ExcludedServices);
            await Save();
            return toggle;
        }

        public async Task Delete(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            if (await Context.GlobalToggles.AnyAsync(t => t.Id == t.Id, cancellationToken))
            {
                Context.GlobalToggles.Remove(toggle);
                Context.ExcludedServices.RemoveRange(toggle.ExcludedServices);
                await Save(cancellationToken);
            }
        }

        public Task<GlobalToggle> GetById(string toggleId, CancellationToken cancellationToken)
        {
            return Context.GlobalToggles.Include(t => t.ExcludedServices).SingleOrDefaultAsync(t => t.Id == toggleId, cancellationToken);
        }

        public Task<List<GlobalToggle>> GetAll(CancellationToken cancellationToken)
        {
            return Context.GlobalToggles.Include(t => t.ExcludedServices).ToListAsync(cancellationToken);
        }

        public async Task<GlobalToggle> Update(GlobalToggle toggle, CancellationToken cancellationToken)
        {
            GlobalToggle existingToggle = await Context.GlobalToggles.Include(t => t.ExcludedServices).SingleOrDefaultAsync(t => t.Id == toggle.Id, cancellationToken);
            existingToggle.Value = toggle.Value;
            existingToggle.Modified = _clockService.UtcNow;
            Context.Update(existingToggle);
            Context.ExcludedServices.RemoveRange(existingToggle.ExcludedServices);
            await Context.ExcludedServices.AddRangeAsync(toggle.ExcludedServices);
            await Save(cancellationToken);

            return existingToggle;
        }
    }
}
