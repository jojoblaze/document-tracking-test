using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TrackingLib;
using TrackingLib.Models;

namespace TrackingMockStorage
{
    public class TrackingMemoryRepository : ITrackingMemoryRepository
    {
        TrackingMemoryStorage _context;


        public TrackingMemoryRepository()
        {
            _context = new TrackingMemoryStorage();
        }


        public Task<DocumentInfo> GetByID(string ID)
        {
            return Task.FromResult<DocumentInfo>(_context.Get(ID));
        }


        public async Task<string> InsertUploadInfoAsync(DocumentInfo uploadInfo)
        {
            return _context.Insert(uploadInfo);
        }


        public async Task InsertDocumentProcessInfoAsync(string id, ProcessInfo processInfo)
        {
            DocumentInfo ui = _context.Get(id);
            ui.Operations ??= new List<ProcessInfo>();
            ui.Operations.Add(processInfo);
        }


        public Task<IEnumerable<DocumentInfo>> GetLastNItemsAsync(int n)
        {
            IEnumerable<DocumentInfo> lastItems = _context.Uploads.OrderByDescending(u => u.UploadDateTime).Take(n);
            return Task.FromResult<IEnumerable<DocumentInfo>>(lastItems);
        }


        public Task<IEnumerable<DocumentInfo>> GetItemsInLastNHoursAsync(int hours)
        {
            IEnumerable<DocumentInfo> lastItems = _context.Uploads.Where(u => u.UploadDateTime >= DateTime.UtcNow.AddHours(-hours)).OrderByDescending(ui => ui.UploadDateTime);
            return Task.FromResult<IEnumerable<DocumentInfo>>(lastItems);
        }



        public Task<IEnumerable<LatestUploadReportEntry>> GenerateStorageReport()
        {
            var reportResult = _context.Uploads.GroupBy(ui => ui.Category).Select(g => new LatestUploadReportEntry { Category = g.Key, DocumentsCount = g.Count(), AllocatedSpace = g.Sum(ui => ui.Size) });
            return Task.FromResult<IEnumerable<LatestUploadReportEntry>>(reportResult);
        }

    }


}
