using AutoMapper;
using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Models;
using TogglerService.Repositories;
using TogglerService.ViewModels;

namespace TogglerService.Commands
{


    public class GetServiceToggleCommand : IGetServiceToggleCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IServiceToggleRepository _serviceToggleRepository;
        private readonly IMapper _mapper;

        public GetServiceToggleCommand(IActionContextAccessor actionContextAccessor, IServiceToggleRepository serviceToggleRepository, IMapper mapper)
        {
            if (actionContextAccessor is null)
                throw new ArgumentNullException(nameof(actionContextAccessor));

            if (serviceToggleRepository is null)
                throw new ArgumentNullException(nameof(serviceToggleRepository));

            if (mapper is null)
                throw new ArgumentNullException(nameof(mapper));

            _actionContextAccessor = actionContextAccessor;
            _serviceToggleRepository = serviceToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId,string serviceId, CancellationToken cancellationToken)
        {
            ServiceToggle toggle = await _serviceToggleRepository.GetById(toggleId, serviceId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            HttpContext httpContext = _actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out Microsoft.Extensions.Primitives.StringValues stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out DateTimeOffset modifiedSince) &&
                    (modifiedSince >= toggle.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            ServiceToggleVM toggleViewModel = _mapper.Map<ServiceToggle,ServiceToggleVM>(toggle);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, toggle.Modified.ToString("R", CultureInfo.InvariantCulture));
            return new OkObjectResult(toggleViewModel);
        }
    }
}
