using System.Threading;
using System.Threading.Tasks;
using TogglerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using TogglerService.Models;

namespace TogglerService.Commands
{


    public class DeleteGlobalToggleCommand : IDeleteGlobalToggleCommand
    {
        private readonly IGlobalToggleRepository _globalToggleRepository;

        public DeleteGlobalToggleCommand(IGlobalToggleRepository globalToggleRepository) =>
            this._globalToggleRepository = globalToggleRepository;

        public async Task<IActionResult> ExecuteAsync(string toggleId, CancellationToken cancellationToken)
        {
            GlobalToggle toggle =  await this._globalToggleRepository.Get(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            await this._globalToggleRepository.Delete(toggle, cancellationToken);

            return new NoContentResult();
        }
    }
}
