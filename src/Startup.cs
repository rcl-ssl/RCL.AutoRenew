using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RCL.AutoRenew.Function.Startup))]
namespace RCL.AutoRenew.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IServiceCollection services = builder.Services;

            IConfiguration configuration = builder.GetContext().Configuration;

            services.AddAuthTokenService(options => configuration.Bind("Auth", options));

            services.AddRCLSDK(options => configuration.Bind("RCLSDK", options));
        }
    }
}
