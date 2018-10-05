using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using TogglerService.ViewModelSchemaFilters;


namespace TogglerService.ViewModels
{
    /// <summary>
    /// A model of toggle.
    /// </summary>
    [SwaggerSchemaFilter(typeof(ToggleVMSchemaFilter))]
    public class ToggleVM
    {
        /// <summary>
        /// The toggles unique identifier.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Id { get; set; }


        /// <summary>
        /// The default value for the toggle
        /// </summary>
        [Required]
        public bool Value { get; set; }

        /// <summary>
        /// Time when toggle is created 
        /// </summary>
        public DateTimeOffset Created { get; set; }

        /// <summary>
        /// Time when toggle is modified 
        /// </summary>
        public DateTimeOffset Modified { get; set; }

    }
}
