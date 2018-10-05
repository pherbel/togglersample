using Microsoft.Extensions.DependencyInjection;
using TogglerService.Commands;
using TogglerService.Repositories;
using TogglerService.Services;

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
                    .AddScoped<IDeleteGlobalToggleCommand, DeleteGlobalToggleCommand>()
                    .AddScoped<IGetGlobalToggleCommand, GetGlobalToggleCommand>()
                    .AddScoped<IGetGlobalTogglesListCommand, GetGlobalTogglesListCommand>()
                    .AddScoped<IPostGlobalToggleCommand, PostGlobalToggleCommand>()
                    .AddScoped<IPutGlobalToggleCommand, PutGlobalToggleCommand>()
                    .AddScoped<IDeleteServiceToggleCommand, DeleteServiceToggleCommand>()
                    .AddScoped<IGetServiceToggleCommand, GetServiceToggleCommand>()
                    .AddScoped<IGetServiceTogglesListCommand, GetServiceTogglesListCommand>()
                    .AddScoped<IGetServiceTogglesListByToggleIdCommand, GetServiceTogglesListByToggleIdCommand>()
                    .AddScoped<IPostServiceToggleCommand, PostServiceToggleCommand>()
                    .AddScoped<IPutServiceToggleCommand, PutServiceToggleCommand>()
                    .AddScoped<IGetTogglesListCommand, GetTogglesListCommand>();
        }

        public static IServiceCollection AddProjectRepositories(this IServiceCollection services)
        {
            return services
                    .AddScoped<IGlobalToggleRepository, GlobalToggleRepository>()
                    .AddScoped<IServiceToggleRepository, ServiceToggleRepository>();
        }

        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            return services
                    .AddSingleton<IClockService, ClockService>()
                    .AddSingleton<IHierarchyRuleEvaluator, BasicHierarchyRuleEvaluator>();
        }
    }
}
