namespace TogglerService.Repositories
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TogglerService.Models;

    public interface IGlobalToggleRepository
    {
        Task<GlobalToggle> Add(GlobalToggle toggle, CancellationToken cancellationToken);

        Task Delete(GlobalToggle toggle, CancellationToken cancellationToken);

        Task<GlobalToggle> GetById(string toggleId, CancellationToken cancellationToken);

        Task<ICollection<GlobalToggle>> GetAll(CancellationToken cancellationToken);

        Task<GlobalToggle> Update(GlobalToggle toggle, CancellationToken cancellationToken);
    }
}
