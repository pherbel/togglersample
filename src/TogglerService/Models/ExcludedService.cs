using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TogglerService.Models
{
    public class ExcludedService
    {

        public GlobalToggle GlobalToggle { get; set; }

        public string ToggleId { get; set; }

        public string ServiceId { get; set; }
    }
}
