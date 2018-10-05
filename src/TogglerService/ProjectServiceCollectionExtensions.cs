using TogglerService.Commands;
using TogglerService.Repositories;
using TogglerService.Services;
using TogglerService.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace TogglerService
{


    /// <summary>
    /// <see cref="IServiceCollection"/> extension methods add project services.
    /// </summary>
    /// <remarks>
    /// AddSingleton - Only one instance is ever created and returned.
    /// AddScoped - A new instance is created and returned for each request/response cycle.
    /// AddTransient - A new instance is created and returned each time.
    /// </remarks>
    public static class ProjectServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectCommands(this IServiceCollection services)
        {
            return services
.AddSingleton<IDeleteGlobalToggleCommand, DeleteGlobalToggleCommand>()
.AddSingleton<IGetGlobalToggleCommand, GetGlobalToggleCommand>()
.AddSingleton<IGetGlobalTogglesListCommand, GetGlobalTogglesListCommand>()
.AddSingleton<IPostGlobalToggleCommand, PostGlobalToggleCommand>()
.AddSingleton<IPutGlobalToggleCommand, PutGlobalToggleCommand>();
        }

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services)
        {
            return services
.AddSingleton<IGlobalToggleRepository, GlobalToggleRepository>();
        }

        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            return services
.AddSingleton<IClockService, ClockService>();
        }
    }
}
