namespace TogglerService.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.Net.Http.Headers;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TogglerService.Commands;
    using TogglerService.Constants;
    using TogglerService.ViewModels;

    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TogglesController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("Global")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
        public IActionResult Options()
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post);
            return Ok();
        }



        /// <summary>
        /// Creates a new global toggle.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggle">The global toggle to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created  global toggle or a 400 Bad Request if the  global toggle is
        /// invalid.</returns>
        [HttpPost("Global", Name = TogglesControllerRoute.PostGlobalToggle)]
        [SwaggerResponse(StatusCodes.Status201Created, "The global toggle was created.", typeof(ToggleVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The global toggle is invalid.", typeof(ModelStateDictionary))]
        public Task<IActionResult> Post(
            [FromServices] IPostGlobalToggleCommand command,
            [FromBody] SaveGlobalToggleVM toggle,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggle, cancellationToken);
        }


        /// <summary>
        /// Gets a collection of global toggles.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of global toggles, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("Global", Name = TogglesControllerRoute.GetGlobalTogglesList)]
        [HttpHead("Global", Name = TogglesControllerRoute.HeadGlobalTogglesList)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of global toggles for the specified page.", typeof(List<ToggleVM>))]
        public Task<IActionResult> GetPage(
            [FromServices] IGetGlobalTogglesListCommand command,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(cancellationToken);
        }


        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a global toggle with the specified unique identifier.
        /// </summary>
        /// <param name="toggleId">The global toggles unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("Global/{toggleId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
#pragma warning disable CA1801 // Naming Styles
        public IActionResult Options(string toggleId)
#pragma warning restore CA1801 // Naming Styles
        {
            HttpContext.Response.Headers.AppendCommaSeparatedValues(
                HeaderNames.Allow,
                HttpMethods.Delete,
                HttpMethods.Get,
                HttpMethods.Head,
                HttpMethods.Options,
                HttpMethods.Post,
                HttpMethods.Put);
            return Ok();
        }

        /// <summary>
        /// Deletes the global toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The toggles unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the global toggle was deleted or a 404 Not Found if a global toggle with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("Global/{toggleId}", Name = TogglesControllerRoute.DeleteGlobalToggle)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The global toggle with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A global toggle with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteGlobalToggleCommand command,
            string toggleId,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, cancellationToken);
        }

        /// <summary>
        /// Gets the global toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The toggles unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the global toggle was deleted or a 404 Not Found if a global toggle with the specified
        /// unique identifier was not found.</returns>
        [HttpGet("Global/{toggleId}", Name = TogglesControllerRoute.GetGlobalToggle)]
        [HttpHead("Global/{toggleId}", Name = TogglesControllerRoute.HeadGlobalToggle)]
        [SwaggerResponse(StatusCodes.Status200OK, "The global toggle with the specified unique identifier.", typeof(ToggleVM))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The global toggle has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A global toggle with the specified unique identifier was not found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetGlobalToggleCommand command,
            string toggleId,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, cancellationToken);
        }


        /// <summary>
        /// Updates an existing global toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The global toggle identifier.</param>
        /// <param name="globalToggle">The global toggle to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated global toggle, a 400 Bad Request if the global toggle is invalid or a
        /// or a 404 Not Found if a global toggle with the specified unique identifier was not found.</returns>
        [HttpPut("Global/{toggleId}", Name = TogglesControllerRoute.PutGlobalToggle)]
        [SwaggerResponse(StatusCodes.Status200OK, "The global toggle was updated.", typeof(GlobalToggleVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The global toggle is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A global toggle with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutGlobalToggleCommand command,
            string toggleId,
            [FromBody] SaveGlobalToggleVM globalToggle,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, globalToggle, cancellationToken);
        }
    }
}
