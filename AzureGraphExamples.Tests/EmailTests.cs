using AzureGraphExamples.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using AzureGraphExamples.Services.Abstract;
using Microsoft.Graph;
using Directory = System.IO.Directory;

namespace AzureGraphExamples.Tests
{


    public class EmailTests
        {

            private readonly IEmailService _emailService;

            public EmailTests()
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
                    .Build();

                _emailService = new EmailService(new MsGraphService(configuration), configuration);

            }

            [Fact]
            public async Task TrySendEmail()
            {
                await _emailService.SendEmail(new Microsoft.Graph.Message()
                {
                    Subject = "Test Email",
                    Body = new ItemBody()
                    {
                        ContentType = BodyType.Text,
                        Content = "This is a test"
                    },
                    ToRecipients = new List<Recipient>()
                    {
                        new Recipient()
                        {
                            EmailAddress = new EmailAddress()
                            {
                                Address = "#_test@github.com"
                            }
                        }
                    }
                });
            }


        }
    
}