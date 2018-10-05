﻿namespace TogglerService
{
    using System;
    using System.IO;
    using System.Reflection;
    using TogglerService.Options;
    using Boxed.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Server.Kestrel.Core;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Core;

    public sealed class Program
    {
        public static int Main(string[] args)
        {
            return LogAndRun(CreateWebHostBuilder(args).Build());
        }

        public static int LogAndRun(IWebHost webHost)
        {
            Log.Logger = BuildLogger(webHost);

            try
            {


                Log.Information("Starting application");
                SeedData.EnsureSeedData(webHost.Services); //TODO: It used only for Local Test
                webHost.Run();
                Log.Information("Stopped application");
                return 0;
            }
            catch (Exception exception)
            {
                Log.Fatal(exception, "Application terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                        .UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build())
                        .UseKestrel(
                        (builderContext, options) =>
                        {
                                                // Do not add the Server HTTP header.
                                                options.AddServerHeader = false;
                                                // Configure Kestrel from appsettings.json.
                                                options.Configure(builderContext.Configuration.GetSection(nameof(ApplicationOptions.Kestrel)));

                                                // Configuring Limits from appsettings.json is not supported. So we manually copy them from config.
                                                // See https://github.com/aspnet/KestrelHttpServer/issues/2216
                                                KestrelServerOptions kestrelOptions = builderContext.Configuration.GetSection<KestrelServerOptions>(nameof(ApplicationOptions.Kestrel));
                        foreach (PropertyInfo property in typeof(KestrelServerLimits).GetProperties())
                        {
                        var value = property.GetValue(kestrelOptions.Limits);
                        property.SetValue(options.Limits, value);
                        }
                        })
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureAppConfiguration((hostingContext, config) =>
                        AddConfiguration(config, hostingContext.HostingEnvironment, args))
                        .UseSerilog()
                        .UseIISIntegration()
                        .UseDefaultServiceProvider((context, options) =>
                        options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                        .UseStartup<Startup>();
        }

        private static IConfigurationBuilder AddConfiguration(
            IConfigurationBuilder configurationBuilder,
            IHostingEnvironment hostingEnvironment,
            string[] args)
        {
            return configurationBuilder
// Add configuration from the appsettings.json file.
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
// Add configuration from an optional appsettings.development.json, appsettings.staging.json or
// appsettings.production.json file, depending on the environment. These settings override the ones in
// the appsettings.json file.
.AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
// This reads the configuration keys from the secret store. This allows you to store connection strings
// and other sensitive settings, so you don't have to check them into your source control provider.
// Only use this in Development, it is not intended for Production use. See
// http://docs.asp.net/en/latest/security/app-secrets.html
.AddIf(
    hostingEnvironment.IsDevelopment(),
    x => x.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true))
// Add configuration specific to the Development, Staging or Production environments. This config can
// be stored on the machine being deployed to or if you are using Azure, in the cloud. These settings
// override the ones in all of the above config files. See
// http://docs.asp.net/en/latest/security/app-secrets.html
.AddEnvironmentVariables()
// Add command line options. These take the highest priority.
.AddIf(
    args != null,
    x => x.AddCommandLine(args));
        }

        private static Logger BuildLogger(IWebHost webHost)
        {
            return new LoggerConfiguration()
.ReadFrom.Configuration(webHost.Services.GetRequiredService<IConfiguration>())
.Enrich.WithProperty("Application", GetAssemblyProductName())
.CreateLogger();
        }

        private static string GetAssemblyProductName()
        {
            return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product;
        }
    }
}
