using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using TrackingLib.Models;

namespace TrackingLib
{

    public class TrackingManager
    {
        ITrackingMemoryRepository _repository;

        public TrackingManager(ITrackingMemoryRepository repository)
        {
            _repository = repository;
        }


        public async Task<DocumentInfo> GetUploadInfoByKey(string externalID)
        {
            return await _repository.GetByID(externalID);
        }


        public virtual async Task<string> TrackDocument(DocumentInfo uploadInfo)
        {
            return await _repository.InsertUploadInfoAsync(uploadInfo);
        }

        public async Task TrackOperation(string id, ProcessInfo processInfo)
        {
            await _repository.InsertDocumentProcessInfoAsync(id, processInfo);
        }

        public async Task<LatestUploadReport> GetLatestUploadsReport(int value, ReportUnit unit)
        {
            if (value == 0)
                throw new ArgumentException("value must be greater than zero.");

            LatestUploadReport report = new LatestUploadReport();

            Task<IEnumerable<DocumentInfo>> uploadsTask = null;
            switch (unit)
            {
                case ReportUnit.Units:
                    uploadsTask = _repository.GetLastNItemsAsync(value);
                    break;
                case ReportUnit.Hours:
                    uploadsTask = _repository.GetItemsInLastNHoursAsync(value);
                    break;
            }

            Task<IEnumerable<LatestUploadReportEntry>> reportTask = _repository.GenerateStorageReport();

            await Task.WhenAll(uploadsTask, reportTask);

            report.Uploads = uploadsTask.Result;
            report.StorageByCategory = reportTask.Result;

            return report;
        }

    }

}
