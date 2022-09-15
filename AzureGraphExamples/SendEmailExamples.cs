using System.Text.Json;
using System.Threading.Tasks;
using AzureGraphExamples.Services;
using AzureGraphExamples.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;

namespace AzureGraphExamples
{
    public class SendEmailExamples
    {

        private readonly IEmailService _emailService;

        public SendEmailExamples(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [FunctionName("SendEmailToKevinK")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Starting To Send Email to Kevin K..");

            var request = await JsonSerializer.DeserializeAsync<Message>(req.Body);

            await _emailService.SendEmail(request);

            log.LogInformation("Ending Send Email to Kevin K..");


            return new OkObjectResult(null);
        }
    }
}