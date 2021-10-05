using AuditSeverityModule.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AuditSeverityModule.Repository
{
    public class SeverityRepo : ISeverityRepo
    {
        public readonly log4net.ILog log4netval = log4net.LogManager.GetLogger(typeof(SeverityRepo));
        public List<AuditBenchmark> GetResponse(string token)
        {
            log4netval.Info(" GetResponse Method of SeverityRepo Called ");
            try
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback +=
                            (sender, certificate, chain, errors) =>
                            {
                                return true;
                            };
                HttpClient client = new HttpClient(handler);
                List<AuditBenchmark> listFromAuditBenchmark = new List<AuditBenchmark>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage response = client.GetAsync("https://localhost:44359/api" + "/AuditBenchmark").Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    listFromAuditBenchmark = JsonConvert.DeserializeObject<List<AuditBenchmark>>(data);
                }
                return listFromAuditBenchmark;

            }
            catch (Exception e)
            {
                log4netval.Error(e.Message);
                return null;
            }

        }
        public static List<AuditResponse> ActionToTakeAndStatus = new List<AuditResponse>()
        {
            new AuditResponse
            {
                RemedialActionDuration="No Action Needed",
                ProjectExexutionStatus="GREEN"
            },
            new AuditResponse
            {
                RemedialActionDuration="Action to be taken in 2 weeks",
                ProjectExexutionStatus="RED"
            },
            new AuditResponse
            {
                RemedialActionDuration = "Action to be taken in 1 week",
                ProjectExexutionStatus="RED"
            }
        };
    }
}
