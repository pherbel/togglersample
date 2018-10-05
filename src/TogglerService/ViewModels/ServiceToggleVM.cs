using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{
    /// <summary>
    /// A model of toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(ServiceToggleVMSchemaFilter))]
    public class ServiceToggleVM : ToggleVM
    {
        /// <summary>
        /// The service unique identifier.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string ServiceId { get; set; }

        /// <summary>
        /// The service version range.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string VersionRange { get; set; }
    }
}
