using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TogglerService.Commands;
using TogglerService.Constants;
using TogglerService.ViewModels;

namespace TogglerService.Controllers
{

    /// <summary>
    /// The status of this API.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RequestController : ControllerBase
    {
#pragma warning disable CA1801 // Naming Styles
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <param name="version">The version of the service.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{serviceId}/{version}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options(string serviceId, string version)
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options);
            return Ok();
        }
#pragma warning restore CA1801 // Naming Styles
        /// <summary>
        /// Gets a collection of Service toggles.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <param name="version">The version of the service.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of Service toggles, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("{serviceId}/{version}", Name = RequestControllerRoute.GetTogglesList)]
        [HttpHead("{serviceId}/{version}", Name = RequestControllerRoute.HeadTogglesList)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Service toggles.", typeof(List<ToggleVM>))]
        public Task<IActionResult> GetAll(
            [FromServices] IGetTogglesListCommand command,
            string serviceId,
            string version,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(serviceId, version, cancellationToken);
        }
    }
}
