using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{


    /// <summary>
    /// A make and model of global toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(SaveGlobalToggleVMSchemaFilter))]
    public class SaveGlobalToggleVM
    {
        /// <summary>
        /// The global toggles unique identifier.
        /// </summary>
        [StringLength(50)]
        public string Id { get; set; }


        /// <summary>
        /// The default value for the global toggle
        /// </summary>
        [Required]
        public bool Value { get; set; }

    }
}
