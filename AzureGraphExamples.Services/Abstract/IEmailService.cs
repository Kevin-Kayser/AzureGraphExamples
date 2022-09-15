using Microsoft.Graph;

namespace AzureGraphExamples.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmail(Message message);
    }
}