using Medium.Application;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Medium.Functions.Startup))]
namespace Medium.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {        
            builder.Services.AddApplication();
        }
    }
}