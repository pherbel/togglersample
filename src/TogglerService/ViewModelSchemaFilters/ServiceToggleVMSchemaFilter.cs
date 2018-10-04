using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using TogglerService.ViewModels;

namespace TogglerService.ViewModelSchemaFilters
{

    public class ServiceToggleVMSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var toggle = new ServiceToggleVM()
            {
                Id = "isButtonBlue",
                Value = true,
                Created = DateTime.UtcNow,
                Modified = DateTime.UtcNow
            };
            model.Default = toggle;
            model.Example = toggle;
        }
    }
}
