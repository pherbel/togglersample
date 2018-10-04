using Swashbuckle.AspNetCore.Annotations;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{
    /// <summary>
    /// A model of toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(ServiceToggleVMSchemaFilter))]
    public class ServiceToggleVM : ToggleVM
    {
    }
}
