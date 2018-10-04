using System;

namespace TogglerService.Models
{
    public class Toggle
    {
        public string Id { get; set; }

        public bool Value { get; set; }

        public DateTimeOffset Created { get; set; }

        public DateTimeOffset Modified { get; set; }
    }
}
