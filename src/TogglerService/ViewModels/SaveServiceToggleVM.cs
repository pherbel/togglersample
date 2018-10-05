using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using TogglerService.ViewModelSchemaFilters;

namespace TogglerService.ViewModels
{

    /// <summary>
    /// A make and model of service toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(SaveServiceToggleVMSchemaFilter))]
    public class SaveServiceToggleVM
    {
        /// <summary>
        /// The service toggles unique identifier.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Id { get; set; }


        /// <summary>
        /// The default value for the service toggle
        /// </summary>
        [Required]
        public bool Value { get; set; }

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
