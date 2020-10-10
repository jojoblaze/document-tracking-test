using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackingAPI.DTO;
using TrackingLib;
using TrackingLib.Models;

namespace TrackingAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("document-tracking")]
    public class DocumentTrackingController : ControllerBase
    {
        private readonly ILogger<DocumentTrackingController> _logger;

        private readonly TrackingManager _manager;

        private readonly IMapper _mapper;



        public DocumentTrackingController(ILogger<DocumentTrackingController> logger, TrackingManager manager, IMapper mapper)
        {
            _logger = logger;
            _manager = manager;
            _mapper = mapper;
        }

        [Route("uploadinfo")]
        [HttpPost]
        [Produces("text/json")]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<ActionResult<UploadInfoResponse>> PostUploadInfo(UploadInfoRequest uploadInfoRequest)
        {
            if (!IsUploadInfoRequestValid(uploadInfoRequest))
                return BadRequest("document data incomplete");

            var uploadInfo = _mapper.Map<DocumentInfo>(uploadInfoRequest);

            UploadInfoResponse response = new UploadInfoResponse();
            response.ExternalID = await _manager.TrackDocument(uploadInfo);
            return Ok(response);
        }

        private static bool IsUploadInfoRequestValid(UploadInfoRequest uploadInfoRequest)
        {
            return uploadInfoRequest != null && string.IsNullOrEmpty(uploadInfoRequest.ID) == false && string.IsNullOrEmpty(uploadInfoRequest.DocumentCategory) == false && uploadInfoRequest.Size > 0;
        }

        [Route("processinfo")]
        [HttpPost]
        [Produces("text/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> PostProcessInfo(ProcessInfoRequest processInfoRequest)
        {
            if (!IsProcessInfoRequestValid(processInfoRequest))
                return BadRequest("process data incomplete");

            var pi = await _manager.GetUploadInfoByKey(processInfoRequest.ExternalID);
            if (pi == null)
                return NotFound();

            var processInfo = _mapper.Map<ProcessInfo>(processInfoRequest);
            await _manager.TrackOperation(processInfoRequest.ExternalID, processInfo);
            return Ok();
        }

        private static bool IsProcessInfoRequestValid(ProcessInfoRequest processInfoRequest)
        {
            return processInfoRequest != null && string.IsNullOrEmpty(processInfoRequest.ID) == false && string.IsNullOrEmpty(processInfoRequest.ExternalID) == false && string.IsNullOrEmpty(processInfoRequest.ProcessType) == false && string.IsNullOrEmpty(processInfoRequest.Result) == false && processInfoRequest.TimeSpent > 0;
        }
    }
}
