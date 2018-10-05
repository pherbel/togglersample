﻿namespace TogglerService
{
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc.ApiExplorer;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    using System.Reflection;
    using TogglerService.Constants;
    using TogglerService.Options;

    public static partial class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds developer friendly error pages for the application which contain extra debug and exception information.
        /// Note: It is unsafe to use this in production.
        /// </summary>
        public static IApplicationBuilder UseDeveloperErrorPages(this IApplicationBuilder application)
        {
            return application
// When a database error occurs, displays a detailed error page with full diagnostic information. It is
// unsafe to use this in production. Uncomment this if using a database.
// .UseDatabaseErrorPage(DatabaseErrorPageOptions.ShowAll);
// When an error occurs, displays a detailed error page with full diagnostic information.
// See http://docs.asp.net/en/latest/fundamentals/diagnostics.html
.UseDeveloperExceptionPage();
        }

        /// <summary>
        /// Uses the static files middleware to serve static files. Also adds the Cache-Control and Pragma HTTP
        /// headers. The cache duration is controlled from configuration.
        /// See http://andrewlock.net/adding-cache-control-headers-to-static-files-in-asp-net-core/.
        /// </summary>
        public static IApplicationBuilder UseStaticFilesWithCacheControl(this IApplicationBuilder application)
        {
            Microsoft.AspNetCore.Mvc.CacheProfile cacheProfile = application
                .ApplicationServices
                .GetRequiredService<CacheProfileOptionsDictionary>()
                .Where(x => string.Equals(x.Key, CacheProfileName.StaticFiles, StringComparison.Ordinal))
                .Select(x => x.Value)
                .SingleOrDefault() ??
                throw new InvalidOperationException("CacheProfiles.StaticFiles section is missing in appsettings.json");
            return application
                .UseStaticFiles(
                    new StaticFileOptions()
                    {
                        OnPrepareResponse = context =>
                        {
                            context.Context.ApplyCacheProfile(cacheProfile);
                        },
                    });
        }


        public static IApplicationBuilder UseCustomSwaggerUI(this IApplicationBuilder application)
        {
            return application.UseSwaggerUI(
                                options =>
                                {
                                    // Set the Swagger UI browser document title.
                                    options.DocumentTitle = typeof(Startup)
                                                                .Assembly
                                                                .GetCustomAttribute<AssemblyProductAttribute>()
                                                                .Product;
                                    // Set the Swagger UI to render at '/'.
                                    options.RoutePrefix = string.Empty;
                                    // Show the request duration in Swagger UI.
                                    options.DisplayRequestDuration();

                                    IApiVersionDescriptionProvider provider = application.ApplicationServices.GetService<IApiVersionDescriptionProvider>();
                                    foreach (ApiVersionDescription apiVersionDescription in provider
                                    .ApiVersionDescriptions
                                    .OrderByDescending(x => x.ApiVersion))
                                    {
                                        options.SwaggerEndpoint(
                                        $"/swagger/{apiVersionDescription.GroupName}/swagger.json",
                                        $"Version {apiVersionDescription.ApiVersion}");
                                    }
                                });
        }
    }
}
