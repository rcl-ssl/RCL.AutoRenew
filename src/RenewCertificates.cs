using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RCL.SDK;

namespace RCL.AutoRenew.Function
{
    public class RenewCertificates
    {
        private readonly ICertificateService _certificateService;

        public RenewCertificates(ICertificateService certificateService)
        {
            _certificateService = certificateService;
        }

        [FunctionName("RenewCertificates")]
        public async Task Run([TimerTrigger("%CRON_EXPRESSION%")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Attemting to automatically renew certificates at {DateTime.Now.ToLongDateString()} ...");

            try
            {
                List<CertificateResponse> certificateResponses = await _certificateService
                    .PostCertificateRenewalAsync(true);

                if (certificateResponses?.Count > 0)
                {
                    string message = $"The following certificates were scheduled for renewal at {DateTime.Now.ToLongDateString()} : {JsonConvert.SerializeObject(certificateResponses)}";
                    log.LogInformation(message);
                }
                else
                {
                    string message = $"There were no certificates found for renewal at  {DateTime.Now.ToLongDateString()}";
                    log.LogInformation(message);
                }
            }
            catch(Exception ex)
            {
                string err = $"Certificate renewal failed at {DateTime.Now.ToLongDateString()}. {ex.Message}";
                log.LogError(err);
            }
        }
    }
}
