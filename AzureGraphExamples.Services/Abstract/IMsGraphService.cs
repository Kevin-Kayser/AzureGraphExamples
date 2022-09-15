using Microsoft.Graph;

namespace AzureGraphExamples.Services.Abstract
{
    public interface IMsGraphService
    {
        public Task<GraphServiceClient> GetGraphServiceClient();
    }
}