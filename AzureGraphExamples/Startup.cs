using System;
using System.Reflection;
using AzureGraphExamples.Services;
using AzureGraphExamples.Services.Abstract;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


[assembly: FunctionsStartup(typeof(AzureGraphExamples.Startup))]
namespace AzureGraphExamples
{
    public class Startup : FunctionsStartup
    {
        // override configure method
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, false)
                .AddEnvironmentVariables()
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddTransient<IMsGraphService, MsGraphService>();
            builder.Services.AddTransient<IEmailService, EmailService>();
        }
    }
}