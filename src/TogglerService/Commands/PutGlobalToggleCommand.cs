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

            this._globalToggleRepository = globalToggleRepository;
            this._mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, SaveGlobalToggleVM saveToggle, CancellationToken cancellationToken)
        {
            var toggle = await this._globalToggleRepository.Get(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            this._mapper.Map(saveToggle, toggle);
            toggle = await this._globalToggleRepository.Update(toggle, cancellationToken);
            var toggleViewModel = this._mapper.Map<GlobalToggle, GlobalToggleVM>(toggle);

            return new OkObjectResult(toggleViewModel);
        }
    }
}
