using System.Threading;
using System.Threading.Tasks;
using TogglerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using TogglerService.Models;
using System;

namespace TogglerService.Commands
{


    public class DeleteGlobalToggleCommand : IDeleteGlobalToggleCommand
    {
        private readonly IGlobalToggleRepository _globalToggleRepository;

        public DeleteGlobalToggleCommand(IGlobalToggleRepository globalToggleRepository)
        {
            if (globalToggleRepository is null)
                throw new ArgumentNullException(nameof(globalToggleRepository));

            _globalToggleRepository = globalToggleRepository;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, CancellationToken cancellationToken)
        {
            GlobalToggle toggle =  await _globalToggleRepository.GetById(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            await _globalToggleRepository.Delete(toggle, cancellationToken);

            return new NoContentResult();
        }
    }
}
