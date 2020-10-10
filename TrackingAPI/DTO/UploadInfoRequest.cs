using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingAPI.DTO
{

    public class UploadInfoRequest
    {
        public string ID { get; set; }

        public string DocumentName { get; set; }

        public string DocumentCategory { get; set; }

        public long Size { get; set; }
    }
}
