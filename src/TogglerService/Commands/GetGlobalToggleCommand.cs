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


    public class GetGlobalToggleCommand : IGetGlobalToggleCommand
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IGlobalToggleRepository _globalToggleRepository;
        private readonly IMapper _mapper;

        public GetGlobalToggleCommand(IActionContextAccessor actionContextAccessor, IGlobalToggleRepository globalToggleRepository, IMapper mapper)
        {
            _actionContextAccessor = actionContextAccessor;
            _globalToggleRepository = globalToggleRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, CancellationToken cancellationToken)
        {
            GlobalToggle toggle = await _globalToggleRepository.GetById(toggleId, cancellationToken);
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

            ToggleVM toggleViewModel = _mapper.Map<GlobalToggle,ToggleVM>(toggle);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, toggle.Modified.ToString("R", CultureInfo.InvariantCulture));
            return new OkObjectResult(toggleViewModel);
        }
    }
}
