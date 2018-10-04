using AutoMapper;
using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using System;
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
            this._actionContextAccessor = actionContextAccessor;
            this._globalToggleRepository = globalToggleRepository;
            this._mapper = mapper;
        }

        public async Task<IActionResult> ExecuteAsync(string toggleId, CancellationToken cancellationToken)
        {
            var toggle = await this._globalToggleRepository.Get(toggleId, cancellationToken);
            if (toggle == null)
            {
                return new NotFoundResult();
            }

            var httpContext = this._actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= toggle.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var toggleViewModel = this._mapper.Map<GlobalToggle,ToggleVM>(toggle);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, toggle.Modified.ToString("R"));
            return new OkObjectResult(toggleViewModel);
        }
    }
}
