using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{
    /// <summary>
    /// A model of toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(GlobalToggleVMSchemaFilter))]
    public class GlobalToggleVM : ToggleVM
    {
        public HashSet<string> ExcludedServices { get; set; }
    }
}
