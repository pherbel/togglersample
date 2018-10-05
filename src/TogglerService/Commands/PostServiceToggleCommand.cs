using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Constants;
using TogglerService.Models;
using TogglerService.Repositories;
using System;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{
    public class PostServiceToggleCommand : IPostServiceToggleCommand
    {
        private readonly IServiceToggleRepository _serviceToggleRepository;
        private readonly IMapper _mapper;


        public PostServiceToggleCommand(IServiceToggleRepository serviceToggleRepository, IMapper mapper)
        {
            if (serviceToggleRepository is null)
                throw new ArgumentNullException(nameof(serviceToggleRepository));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            _serviceToggleRepository = serviceToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveServiceToggleVM createToggle, CancellationToken cancellationToken)
        {
            ServiceToggle toggle = _mapper.Map<SaveServiceToggleVM, ServiceToggle>(createToggle);
            toggle = await _serviceToggleRepository.Add(toggle, cancellationToken);
            ServiceToggleVM toggleViewModel = _mapper.Map<ServiceToggle, ServiceToggleVM>(toggle);

            return new CreatedAtRouteResult(
                TogglesControllerRoute.GetServiceToggle,
                new { toggleId = toggleViewModel.Id },
                toggleViewModel);
        }
    }
}
