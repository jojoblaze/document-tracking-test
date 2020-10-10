
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingLib.Models;

namespace TrackingLib
{
    public interface ITrackingMemoryRepository
    {
        /// <summary>
        /// Returns upload info by document ID.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        Task<DocumentInfo> GetByID(string ID);

        /// <summary>
        /// Save upload info.
        /// </summary>
        /// <param name="uploadInfo"></param>
        Task InsertDocumentProcessInfoAsync(string id, ProcessInfo processInfo);

        /// <summary>
        /// Save document upload info.
        /// </summary>
        /// <param name="uploadInfo"></param>
        /// <returns>ExternalID</returns>
        Task<string> InsertUploadInfoAsync(DocumentInfo uploadInfo);

        /// <summary>
        /// Returns the most recent <paramref name="n"/> uploads.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        Task<IEnumerable<DocumentInfo>> GetLastNItemsAsync(int n);

        /// <summary>
        /// Returns uploads in last <paramref name="hours"/> hours.
        /// </summary>
        /// <param name="hours"></param>
        /// <returns></returns>
        Task<IEnumerable<DocumentInfo>> GetItemsInLastNHoursAsync(int hours);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LatestUploadReportEntry>> GenerateStorageReport();
    }
}