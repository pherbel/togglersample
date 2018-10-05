using AutoMapper;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class PutGlobalToggleCommand : IPutGlobalToggleCommand
    {
        private readonly IGlobalToggleRepository _globalToggleRepository;
        private readonly IMapper _mapper;

        public PutGlobalToggleCommand(IGlobalToggleRepository globalToggleRepository, IMapper mapper)
        {
            if (globalToggleRepository is null)
                throw new ArgumentNullException(nameof(globalToggleRepository));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            _globalToggleRepository = globalToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, SaveGlobalToggleVM saveToggle, CancellationToken cancellationToken)
        {
            GlobalToggle toggle = await _globalToggleRepository.GetById(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            _mapper.Map(saveToggle, toggle);
            toggle = await _globalToggleRepository.Update(toggle, cancellationToken);
            GlobalToggleVM toggleViewModel = _mapper.Map<GlobalToggle, GlobalToggleVM>(toggle);

            return new OkObjectResult(toggleViewModel);
        }
    }
}
