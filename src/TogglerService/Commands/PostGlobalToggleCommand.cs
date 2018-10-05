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
    public class PostGlobalToggleCommand : IPostGlobalToggleCommand
    {
        private readonly IGlobalToggleRepository _globalToggleRepository;
        private readonly IMapper _mapper;


        public PostGlobalToggleCommand(IGlobalToggleRepository globalToggleRepository, IMapper mapper)
        {
            if (globalToggleRepository is null)
                throw new ArgumentNullException(nameof(globalToggleRepository));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            _globalToggleRepository = globalToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(SaveGlobalToggleVM createToggle, CancellationToken cancellationToken)
        {
            GlobalToggle toggle = _mapper.Map<SaveGlobalToggleVM, GlobalToggle>(createToggle);
            toggle = await _globalToggleRepository.Add(toggle, cancellationToken);
            GlobalToggleVM toggleViewModel = _mapper.Map<GlobalToggle, GlobalToggleVM>(toggle);

            return new CreatedAtRouteResult(
                TogglesControllerRoute.GetGlobalToggle,
                new { toggleId = toggleViewModel.Id },
                toggleViewModel);
        }
    }
}
