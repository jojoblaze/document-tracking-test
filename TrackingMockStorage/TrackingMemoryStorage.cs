using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using TrackingLib;
using TrackingLib.Models;
using System.Linq.Expressions;

namespace TrackingMockStorage
{
    class TrackingMemoryStorage
    {
        Dictionary<string, DocumentInfo> storage { get; set; }


        public TrackingMemoryStorage()
        {
            storage = new Dictionary<string, DocumentInfo>();
        }

        public DocumentInfo Get(string ID)
        {
            DocumentInfo docInfo = null;
            if (storage.ContainsKey(ID))
                docInfo = storage[ID];

            return docInfo;
        }

        public IEnumerable<DocumentInfo> Uploads { get { return storage.Values; } }


        public string Insert(DocumentInfo uploadInfo)
        {
            string externalID = DateTime.Now.ToString("yyyyMMddHHmmss");
            uploadInfo.ExternalID = externalID;
            uploadInfo.UploadDateTime = DateTime.UtcNow;
            storage.Add(externalID, uploadInfo);
            return externalID;
        }
    }

}
