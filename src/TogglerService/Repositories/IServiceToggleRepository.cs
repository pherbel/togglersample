using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;

namespace TogglerService.Repositories
{

    public interface IServiceToggleRepository
    {
        Task<ServiceToggle> Add(ServiceToggle toggle, CancellationToken cancellationToken);

        Task Delete(ServiceToggle toggle, CancellationToken cancellationToken);

        Task<ServiceToggle> GetById(string toggleId, CancellationToken cancellationToken);

        Task<ICollection<ServiceToggle>> GetAll( CancellationToken cancellationToken);

        Task<ServiceToggle> Update(ServiceToggle toggle, CancellationToken cancellationToken);
    }
}
