using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrackingAPI.DTO
{

    public class ProcessInfoRequest
    {
        public string ID { get; set; }

        public string ExternalID { get; set; }

        public string ProcessType { get; set; }

        public long TimeSpent { get; set; }

        public string Result { get; set; }
    }

}
