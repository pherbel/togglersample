using System.Collections.Generic;

namespace TogglerService.Models
{
    public class GlobalToggle : Toggle
    {

#pragma warning disable CA2227 // Collection properties should be read only
        public List<ExcludedService> ExcludedServices { get; set; } = new List<ExcludedService>();
#pragma warning restore CA2227 // Collection properties should be read only

    }
}
