using AuditSeverityModule.Models;
using AuditSeverityModule.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuditSeverityModule.Services
{
    public class SeverityService : ISeverityService
    {
        private ISeverityRepo severityRepo;
        public readonly log4net.ILog log4netval = log4net.LogManager.GetLogger(typeof(SeverityService));
        public SeverityService(ISeverityRepo severityRepo)
        {
            this.severityRepo = severityRepo;
        }


        public AuditResponse GetSeverityResponse(AuditRequest req, string token)
        {
            log4netval.Info("GetSeverityResponse Method of SeverityService Called ");
            try
            {
                List<AuditBenchmark> listFromRepository = severityRepo.GetResponse(token);

                int count = 0;
                int acceptableNo = 0;

                if (req.Auditdetails.Questions.Question1 == false)
                    count++;
                if (req.Auditdetails.Questions.Question2 == false)
                    count++;
                if (req.Auditdetails.Questions.Question3 == false)
                    count++;
                if (req.Auditdetails.Questions.Question4 == false)
                    count++;
                if (req.Auditdetails.Questions.Question5 == false)
                    count++;

                if (req.Auditdetails.Type == listFromRepository[0].AuditType)
                    acceptableNo = listFromRepository[0].BenchmarkNoAnswers;
                else if (req.Auditdetails.Type == listFromRepository[1].AuditType)
                    acceptableNo = listFromRepository[1].BenchmarkNoAnswers;


                Random randomNumber = new Random();


                AuditResponse auditResponse = new AuditResponse();
                if (req.Auditdetails.Type == "Internal" && count <= acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = Repository.SeverityRepo.ActionToTakeAndStatus[0].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = Repository.SeverityRepo.ActionToTakeAndStatus[0].RemedialActionDuration;
                }
                else if (req.Auditdetails.Type == "Internal" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = Repository.SeverityRepo.ActionToTakeAndStatus[1].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = Repository.SeverityRepo.ActionToTakeAndStatus[1].RemedialActionDuration;
                }
                else if (req.Auditdetails.Type == "SOX" && count <= acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = Repository.SeverityRepo.ActionToTakeAndStatus[0].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = Repository.SeverityRepo.ActionToTakeAndStatus[0].RemedialActionDuration;
                }
                else if (req.Auditdetails.Type == "SOX" && count > acceptableNo)
                {
                    auditResponse.AuditId = randomNumber.Next();
                    auditResponse.ProjectExexutionStatus = Repository.SeverityRepo.ActionToTakeAndStatus[2].ProjectExexutionStatus;
                    auditResponse.RemedialActionDuration = Repository.SeverityRepo.ActionToTakeAndStatus[2].RemedialActionDuration;
                }


                return auditResponse;
            }
            catch (Exception ex)
            {
                log4netval.Error(ex.Message);
                return null;
            }


        }
    }
    
}
