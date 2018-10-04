using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using TogglerService.ViewModels;


namespace TogglerService.ViewModelSchemaFilters
{

    public class SaveGlobalToggleVMSchemaFilter : ISchemaFilter
    {
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var globalToggle = new SaveGlobalToggleVM()
            {
                Id = "isButtonBlue",
                Value = true
            };
            model.Default = globalToggle;
            model.Example = globalToggle;
        }
    }
}
