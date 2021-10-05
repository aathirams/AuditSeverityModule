using AuditSeverityModule.Models;
using AuditSeverityModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuditSeverityController : ControllerBase
    {
        private ISeverityService severityService;
        public readonly log4net.ILog log4netval = log4net.LogManager.GetLogger(typeof(AuditSeverityController));
        public AuditSeverityController(ISeverityService severityService)
        {
            this.severityService = severityService;
        }

        [HttpGet]
        public IActionResult GetProjectExecutionStatus()
        {
            log4netval.Info(" Http GET Request From: " + nameof(AuditSeverityController));

            return Ok("SUCCESS");
        }



            [HttpPost]
            public IActionResult GetProjectExecutionStatus([FromBody] AuditRequest request)
            {
            log4netval.Info(" Http POST Request From: " + nameof(AuditSeverityController));

            if (request == null)
                return BadRequest();

            else if (request.Auditdetails.Type != "Internal" && request.Auditdetails.Type != "SOX")
                return BadRequest("Wrong Audit Type");
            else
                try
                {
                    string token = HttpContext.Request.Headers["Authorization"].Single().Split(" ")[1];
                    AuditResponse Response = severityService.GetSeverityResponse(request, token);
                    return Ok(Response);
                }
                catch (Exception e)
                {
                    log4netval.Error(e.Message);
                    return StatusCode(500);
                }

        }
    }
}
