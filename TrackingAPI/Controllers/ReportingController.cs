using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackingLib;
using TrackingLib.Models;

namespace TrackingAPI.Controllers
{

    public class ReportRequest
    {
        public int Quantity { get; set; }

        public ReportUnit Unit { get; set; }
    }

    //[Authorize]
    [ApiController]
    [Route("reporting")]
    public class ReportingController : ControllerBase
    {
        private readonly ILogger<ReportingController> _logger;

        private TrackingManager _manager;

        public ReportingController(ILogger<ReportingController> logger, TrackingManager manager)
        {
            _logger = logger;
            _manager = manager;
        }

        [Route("latest-uploads")]
        [HttpGet]
        public async Task<ActionResult<LatestUploadReport>> GetLatestUploads([FromQuery]ReportRequest reportRequest)
        {
            if (!IsReportRequestValid(reportRequest))
                return BadRequest("document data incomplete");

            LatestUploadReport report = await _manager.GetLatestUploadsReport(reportRequest.Quantity, reportRequest.Unit);
            return Ok(report);
        }

        private static bool IsReportRequestValid(ReportRequest reportRequest)
        {
            return reportRequest != null && reportRequest.Quantity > 0;
        }

    }
}
