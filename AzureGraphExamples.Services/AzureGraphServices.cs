using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;
using AzureGraphExamples.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace AzureGraphExamples.Services
{
    /// <summary>
    /// TenantId = Azure Tenant,
    /// clientId = App Registration with permissions to graph api
    /// ClientSecret = Generated manually from the registered app
    ///
    /// Locally it's going to be best to pull this data from UserSecrets so that the credentials are not committed to sourcecontrol.
    ///
    /// In the cloud, container, or on the server should set environmental variables outside of source control.
    /// </summary>
    public class MsGraphService : IMsGraphService
    {
        private readonly string _tenantId;
        private readonly string _clientId;
        private readonly string _clientSecret;
        public MsGraphService(IConfiguration configuration)
        {
            _tenantId = configuration.GetSection("tenantId").Value;
            _clientId = configuration.GetSection("clientId").Value;
            _clientSecret = configuration.GetSection("clientSecret").Value;
        }
        public async Task<GraphServiceClient> GetGraphServiceClient()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var confidentialClient = ConfidentialClientApplicationBuilder
                .Create(_clientId)
                .WithClientSecret(_clientSecret)
                .WithTenantId(_tenantId)
                .Build();

            var graphServiceClient =
                new GraphServiceClient(new DelegateAuthenticationProvider(async (requestMessage) =>
                {
                    var authResult = await confidentialClient.AcquireTokenForClient(scopes).ExecuteAsync();
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", authResult.AccessToken);

                })
                );
            return graphServiceClient;
        }
    }
}