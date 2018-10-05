using System.Collections.Generic;

namespace TogglerService.Options
{
    /// <summary>
    /// The dynamic response compression options for the application.
    /// </summary>
    public class CompressionOptions
    {
#pragma warning disable CA2227 // Collection properties should be read only
        /// <summary>
        /// Gets or sets a list of MIME types to be compressed in addition to the default set used by ASP.NET Core.
        /// </summary>
        public List<string> MimeTypes { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only
    }
}
