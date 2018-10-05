using AutoMapper;
using Boxed.AspNetCore;
using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Constants;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class GetGlobalTogglesListCommand : IGetGlobalTogglesListCommand
    {
        private readonly IGlobalToggleRepository _globalToggleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetGlobalTogglesListCommand(IGlobalToggleRepository globalToggleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _globalToggleRepository = globalToggleRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            ICollection<GlobalToggle> toggles = await _globalToggleRepository.GetAll(cancellationToken);
            if (toggles == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(_mapper.Map<ICollection<GlobalToggle>, List<GlobalToggleVM>>(toggles));
        }
    }
}
