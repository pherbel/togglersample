using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using TogglerService.Data;
using TogglerService.Models;

namespace TogglerService
{
    public static class SeedData
    {
        public static void EnsureSeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {

                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                //It is need only for Local SQL testing
                // context.Database.Migrate();
                EnsureSeedData(context);
            }
        }
        private static void EnsureSeedData(ApplicationDbContext context)
        {
            Console.WriteLine("Seeding database...");

            if (!context.GlobalToggles.Any())
            {
                Console.WriteLine("Global toggles being populated");
                ExcludedService service = new ExcludedService
                {
                    ToggleId = "isButtonRed",
                    ServiceId = "ABC"

                };
                context.ExcludedServices.Add(service);
                context.GlobalToggles.Add(new GlobalToggle()
                {
                    Id = "isButtonBlue",
                    Value = true,
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                });
                context.GlobalToggles.Add(new GlobalToggle()
                {
                    Id = "isButtonRed",
                    Value = false,
                    ExcludedServices = new List<ExcludedService> { service },
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                });

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Global toggles already populated");
            }


            if (!context.ServiceToggles.Any())
            {
                Console.WriteLine("Service toggles populated");
                context.ServiceToggles.Add(new ServiceToggle()
                {
                    Id = "isButtonBlue",
                    ServiceId = "ABC",
                    Value = false,
                    VersionRange = "*",
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                });
                context.ServiceToggles.Add(new ServiceToggle()
                {
                    Id = "isButtonGreen",
                    ServiceId = "ABC",
                    Value = true,
                    VersionRange = "*",
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Service toggles already populated");
            }

            Console.WriteLine("Done seeding database.");
            Console.WriteLine();
        }

    }
}
