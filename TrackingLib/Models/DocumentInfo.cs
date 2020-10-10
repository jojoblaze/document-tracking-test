using System;
using System.Collections.Generic;

namespace TrackingLib.Models
{
    /// <summary>
    /// Track information for an uploaded document.
    /// </summary>
    public class DocumentInfo
    {
        public string ID { get; set; }

        public string ExternalID { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public long Size { get; set; }

        public List<ProcessInfo> Operations { get; set; }

        public DateTime UploadDateTime { get; set; }
    }


}
