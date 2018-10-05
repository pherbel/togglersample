﻿using AutoMapper;
using Boxed.AspNetCore;
using CorrelationId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TogglerService.Constants;

namespace TogglerService
{

    /// <summary>
    /// The main start-up class for the application.
    /// </summary>
    public class Startup : IStartup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The application configuration, where key value pair settings are stored. See
        /// http://docs.asp.net/en/latest/fundamentals/configuration.html</param>
        /// <param name="hostingEnvironment">The environment the application is running under. This can be Development,
        /// Staging or Production by default. See http://docs.asp.net/en/latest/fundamentals/environments.html</param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Configures the services to add to the ASP.NET Core Injection of Control (IoC) container. This method gets
        /// called by the ASP.NET runtime. See
        /// http://blogs.msdn.com/b/webdev/archive/2014/06/17/dependency-injection-in-asp-net-vnext.aspx
        /// </summary>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services
                    .AddCorrelationIdFluent()
                    .AddCustomCaching()
                    .AddCustomOptions(configuration)
                    .AddCustomRouting()
                    .AddResponseCaching()
                    .AddCustomResponseCompression()
                    .AddCustomStrictTransportSecurity()
                    .AddCustomSwagger()
                    .AddHttpContextAccessor()
                    // Add useful interface for accessing the ActionContext outside a controller.
                    .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
                    // Add useful interface for accessing the IUrlHelper outside a controller.
                    .AddScoped(x => x
                        .GetRequiredService<IUrlHelperFactory>()
                        .GetUrlHelper(x.GetRequiredService<IActionContextAccessor>().ActionContext))
                    .AddCustomApiVersioning()
                    .AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddApiExplorer()
                    .AddAuthorization()
                    .AddDataAnnotations()
                    .AddJsonFormatters()
                    .AddCustomJsonOptions(hostingEnvironment)
                    .AddCustomCors()
                    .AddVersionedApiExplorer(x => x.GroupNameFormat = "'v'VVV") // Version format: 'v'major[.minor][-status]
                    .AddCustomMvcOptions()
                    .Services
                    .AddAutoMapper(c =>
                    {

                    }, typeof(Startup))
                    .AddProjectCommands()
                    .AddProjectRepositories()
                    .AddProjectServices()
                    .BuildServiceProvider();
        }

        /// <summary>
        /// Configures the application and HTTP request pipeline. Configure is called after ConfigureServices is
        /// called by the ASP.NET runtime.
        /// </summary>
        public void Configure(IApplicationBuilder application)
        {
            application
            // Pass a GUID in a X-Correlation-ID HTTP header to set the HttpContext.TraceIdentifier.
            .UseCorrelationId()
            .UseForwardedHeaders()
            .UseResponseCaching()
            .UseResponseCompression()
            .UseCors(CorsPolicyName.AllowAny)
            .UseIf(
            !hostingEnvironment.IsDevelopment(),
            x => x.UseHsts())
            .UseIf(
            hostingEnvironment.IsDevelopment(),
            x => x.UseDeveloperErrorPages())
            .UseStaticFilesWithCacheControl()
            .UseMvc()
            .UseSwagger()
            .UseCustomSwaggerUI();

           application.ApplicationServices.GetRequiredService<IMapper>().ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
