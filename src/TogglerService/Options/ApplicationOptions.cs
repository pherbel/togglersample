namespace TogglerService.Options
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Server.Kestrel.Core;

    /// <summary>
    /// All options for the application.
    /// </summary>
    public class ApplicationOptions
    {
#pragma warning disable CA2227 // Collection properties should be read only
        public CacheProfileOptionsDictionary CacheProfiles { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        public CompressionOptions Compression { get; set; }

        public ForwardedHeadersOptions ForwardedHeaders { get; set; }

        public KestrelServerOptions Kestrel { get; set; }
    }
}
