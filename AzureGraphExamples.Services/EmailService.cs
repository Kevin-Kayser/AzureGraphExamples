using System.Data;
using AzureGraphExamples.Services.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;

namespace AzureGraphExamples.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMsGraphService _graphService;
        private readonly string _noReplyUserId;

        public EmailService(IMsGraphService graphService, IConfiguration configuration)
        {
            _noReplyUserId = configuration["noReplyUserId"];
            _graphService = graphService;
        }

        public async Task SendEmail(Message message)
        {
            if (message == default)
            {
                throw new NoNullAllowedException("Message cannot be null");
            }

            var graphClient = await _graphService.GetGraphServiceClient();

            var saveToSentItems = false;

            await graphClient
                .Users[_noReplyUserId]
                .SendMail(message, saveToSentItems)
                .Request()
                .PostAsync();

        }
    }
}