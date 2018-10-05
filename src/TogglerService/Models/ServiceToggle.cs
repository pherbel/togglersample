namespace TogglerService.Models
{
    public class ServiceToggle : Toggle
    {
        public string ServiceId { get; set; }

        public string VersionRange { get; set; }
    }
}
