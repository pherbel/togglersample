using Swashbuckle.AspNetCore.Annotations;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{
    /// <summary>
    /// A model of toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(GlobalToggleVMSchemaFilter))]
    public class GlobalToggleVM : ToggleVM
    {
    }
}
