using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.Services;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class GetTogglesListCommand : IGetTogglesListCommand
    {
        private readonly IServiceToggleRepository _serviceToggleRepository;
        private readonly IGlobalToggleRepository _globalToggleRepository;
        private readonly IHierarchyRuleEvaluator _ruleEvaluator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTogglesListCommand(IServiceToggleRepository serviceToggleRepository, IGlobalToggleRepository globalToggleRepository, IHierarchyRuleEvaluator ruleEvaluator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            if (serviceToggleRepository is null)
            {
                throw new ArgumentNullException(nameof(serviceToggleRepository));
            }

            if (globalToggleRepository is null)
            {
                throw new ArgumentNullException(nameof(globalToggleRepository));
            }

            if (ruleEvaluator is null)
            {
                throw new ArgumentNullException(nameof(ruleEvaluator));
            }

            if (mapper is null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            _serviceToggleRepository = serviceToggleRepository;
            _globalToggleRepository = globalToggleRepository;
            _ruleEvaluator = ruleEvaluator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> ExecuteAsync(string serviceId, string version, CancellationToken cancellationToken)
        {
            List<GlobalToggle> globalToggles = await _globalToggleRepository.GetAll(cancellationToken);
            List<ServiceToggle> serviceToggles = await _serviceToggleRepository.GetAllByServiceId(serviceId, cancellationToken);
            if (globalToggles is null || serviceToggles is null)
            {
                return new NotFoundResult();
            }
            List<Toggle> resultToggles = _ruleEvaluator.Eval(serviceId, version, globalToggles, serviceToggles);

            return new OkObjectResult(_mapper.Map<ICollection<Toggle>, List<ToggleVM>>(resultToggles));
        }
    }
}
