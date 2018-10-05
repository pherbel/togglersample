using System.Threading;
using System.Threading.Tasks;
using TogglerService.Repositories;
using Microsoft.AspNetCore.Mvc;
using TogglerService.Models;
using System;

namespace TogglerService.Commands
{


    public class DeleteServiceToggleCommand : IDeleteServiceToggleCommand
    {
        private readonly IServiceToggleRepository _serviceToggleRepository;

        public DeleteServiceToggleCommand(IServiceToggleRepository serviceToggleRepository)
        {
            if (serviceToggleRepository is null)
                throw new ArgumentNullException(nameof(serviceToggleRepository));

            _serviceToggleRepository = serviceToggleRepository;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, CancellationToken cancellationToken)
        {
            ServiceToggle toggle =  await _serviceToggleRepository.GetById(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            await _serviceToggleRepository.Delete(toggle, cancellationToken);

            return new NoContentResult();
        }
    }
}
