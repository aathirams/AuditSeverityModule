using AuditSeverityModule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityModule.Services
{
    public interface ISeverityService
    {
        public AuditResponse GetSeverityResponse(AuditRequest req, string token);
    }
}
