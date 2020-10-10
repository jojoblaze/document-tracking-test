using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TrackingLib.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReportUnit
    {
        Hours = 'h',
        Units = 'n'
    }

    public class LatestUploadReport
    {
        public IEnumerable<DocumentInfo> Uploads { get; set; }

        public IEnumerable<LatestUploadReportEntry> StorageByCategory { get; set; }
    }

    public class LatestUploadReportEntry
    {
        public string Category { get; set; }

        public long DocumentsCount { get; set; }

        public long AllocatedSpace { get; set; }
    }

}
