namespace TogglerService.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TogglerService.Constants;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    /// <summary>
    /// The status of this API.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StatusController : ControllerBase
    {
        private IEnumerable<IHealthChecker> healthCheckers;

        public StatusController(IEnumerable<IHealthChecker> healthCheckers) =>
            this.healthCheckers = healthCheckers;

        /// <summary>
        /// Gets the status of this API and it's dependencies, giving an indication of it's health.
        /// </summary>
        /// <returns>A 200 OK or error response containing details of what is wrong.</returns>
        [HttpGet(Name = StatusControllerRoute.GetStatus)]
        [AllowAnonymous]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The API is functioning normally.")]
        [SwaggerResponse(StatusCodes.Status503ServiceUnavailable, "The API or one of it's dependencies is not functioning, the service is unavailable.")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var tasks = new List<Task>();

                foreach (IHealthChecker healthChecker in healthCheckers)
                {
                    tasks.Add(healthChecker.CheckHealth());
                }

                await Task.WhenAll(tasks);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status503ServiceUnavailable);
            }

            return new NoContentResult();
        }
    }
}