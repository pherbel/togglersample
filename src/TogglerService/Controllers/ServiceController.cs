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

    [Route("Manage/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class ServiceController : ControllerBase
    {
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods.
        /// </summary>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("")]
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
        /// Creates a new Service toggle.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggle">The Service toggle to create.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 201 Created response containing the newly created  Service toggle or a 400 Bad Request if the  Service toggle is
        /// invalid.</returns>
        [HttpPost("", Name = ServiceControllerRoute.PostServiceToggle)]
        [SwaggerResponse(StatusCodes.Status201Created, "The Service toggle was created.", typeof(ServiceToggleVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Service toggle is invalid.", typeof(ModelStateDictionary))]
        public Task<IActionResult> Post(
            [FromServices] IPostServiceToggleCommand command,
            [FromBody] SaveServiceToggleVM toggle,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggle, cancellationToken);
        }


        /// <summary>
        /// Gets a collection of Service toggles.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of Service toggles, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("", Name = ServiceControllerRoute.GetServiceTogglesList)]
        [HttpHead("", Name = ServiceControllerRoute.HeadServiceTogglesList)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Service toggles.", typeof(List<ServiceToggleVM>))]
        public Task<IActionResult> GetAll(
            [FromServices] IGetServiceTogglesListCommand command,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(cancellationToken);
        }

        /// <summary>
        /// Gets a collection of Service toggles.
        /// </summary>
        /// <param name="toggleId">The Service toggles unique identifier.</param>
        /// <param name="command">The action command.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing a collection of Service toggles, a 400 Bad Request if the page request
        /// parameters are invalid or a 404 Not Found if a page with the specified page number was not found.
        /// </returns>
        [HttpGet("{toggleId}", Name = ServiceControllerRoute.GetServiceTogglesListById)]
        [HttpHead("{toggleId}", Name = ServiceControllerRoute.HeadServiceTogglesListById)]
        [SwaggerResponse(StatusCodes.Status200OK, "A collection of Service toggles for specific toggle.", typeof(List<ServiceToggleVM>))]
        public Task<IActionResult> GetAllForToggleId(
            [FromServices] IGetServiceTogglesListByToggleIdCommand command,
            string toggleId,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, cancellationToken);
        }
        /// <summary>
        /// Returns an Allow HTTP header with the allowed HTTP methods for a Service toggle with the specified unique identifier.
        /// </summary>
        /// <param name="toggleId">The Service toggles unique identifier.</param>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <returns>A 200 OK response.</returns>
        [HttpOptions("{toggleId}/{serviceId}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The allowed HTTP methods.")]
#pragma warning disable CA1801 // Naming Styles
        public IActionResult Options(string toggleId, string serviceId)
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
        /// Deletes the Service toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The toggles unique identifier.</param>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the Service toggle was deleted or a 404 Not Found if a Service toggle with the specified
        /// unique identifier was not found.</returns>
        [HttpDelete("{toggleId}/{serviceId}", Name = ServiceControllerRoute.DeleteServiceToggle)]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The Service toggle with the specified unique identifier was deleted.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Service toggle with the specified unique identifier was not found.")]
        public Task<IActionResult> Delete(
            [FromServices] IDeleteServiceToggleCommand command,
            string toggleId, string serviceId,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, serviceId, cancellationToken);
        }

        /// <summary>
        /// Gets the Service toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The toggles unique identifier.</param>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 204 No Content response if the Service toggle was deleted or a 404 Not Found if a Service toggle with the specified
        /// unique identifier was not found.</returns>
        [HttpGet("{toggleId}/{serviceId}", Name = ServiceControllerRoute.GetServiceToggle)]
        [HttpHead("{toggleId}/{serviceId}", Name = ServiceControllerRoute.HeadServiceToggle)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Service toggle with the specified unique identifier.", typeof(ServiceToggleVM))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The Service toggle has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Service toggle with the specified unique identifier was not found.")]
        public Task<IActionResult> Get(
            [FromServices] IGetServiceToggleCommand command,
            string toggleId, string serviceId,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, serviceId, cancellationToken);
        }


        /// <summary>
        /// Updates an existing Service toggle with the specified unique identifier.
        /// </summary>
        /// <param name="command">The action command.</param>
        /// <param name="toggleId">The Service toggle identifier.</param>
        /// <param name="serviceId">The services unique identifier.</param>
        /// <param name="ServiceToggle">The Service toggle to update.</param>
        /// <param name="cancellationToken">The cancellation token used to cancel the HTTP request.</param>
        /// <returns>A 200 OK response containing the newly updated Service toggle, a 400 Bad Request if the Service toggle is invalid or a
        /// or a 404 Not Found if a Service toggle with the specified unique identifier was not found.</returns>
        [HttpPut("{toggleId}/{serviceId}", Name = ServiceControllerRoute.PutServiceToggle)]
        [SwaggerResponse(StatusCodes.Status200OK, "The Service toggle was updated.", typeof(ServiceToggleVM))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The Service toggle is invalid.", typeof(ModelStateDictionary))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A Service toggle with the specified unique identifier could not be found.")]
        public Task<IActionResult> Put(
            [FromServices] IPutServiceToggleCommand command,
            string toggleId, string serviceId,
            [FromBody] SaveServiceToggleVM ServiceToggle,
            CancellationToken cancellationToken)
        {
            return command.ExecuteAsync(toggleId, serviceId, ServiceToggle, cancellationToken);
        }
    }
}
