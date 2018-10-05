using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class PutServiceToggleCommand : IPutServiceToggleCommand
    {
        private readonly IServiceToggleRepository _serviceToggleRepository;
        private readonly IMapper _mapper;

        public PutServiceToggleCommand(IServiceToggleRepository serviceToggleRepository, IMapper mapper)
        {
            if (serviceToggleRepository is null)
            {
                throw new ArgumentNullException(nameof(serviceToggleRepository));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _serviceToggleRepository = serviceToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, string serviceId, SaveServiceToggleVM saveToggle, CancellationToken cancellationToken)
        {
            ServiceToggle toggle = await _serviceToggleRepository.GetById(toggleId, serviceId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(saveToggle, toggle);
            toggle = await _serviceToggleRepository.Update(toggle, cancellationToken);
            ServiceToggleVM toggleViewModel = _mapper.Map<ServiceToggle, ServiceToggleVM>(toggle);

            return new OkObjectResult(toggleViewModel);
        }
    }
}
