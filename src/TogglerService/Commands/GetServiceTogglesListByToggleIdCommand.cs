using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class GetServiceTogglesListByToggleIdCommand : IGetServiceTogglesListByToggleIdCommand
    {
        private readonly IServiceToggleRepository _serviceToggleRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetServiceTogglesListByToggleIdCommand(IServiceToggleRepository serviceToggleRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

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
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> ExecuteAsync(string toggleId,CancellationToken cancellationToken)
        {
            ICollection<ServiceToggle> toggles = await _serviceToggleRepository.GetAllByToggleId(toggleId,cancellationToken);
            if (toggles == null)
            {
                return new NotFoundResult();
            }
            return new OkObjectResult(_mapper.Map<ICollection<ServiceToggle>, List<ServiceToggleVM>>(toggles));
        }
    }
}
