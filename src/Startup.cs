using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(RCL.LetsEncrypt.AutoRenew.Function.Startup))]
namespace RCL.LetsEncrypt.AutoRenew.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IServiceCollection services = builder.Services;

            IConfiguration configuration = builder.GetContext().Configuration;

            services.AddAuthTokenService(options => configuration.Bind("Auth", options));

            services.AddLetsEncryptSDK(options => configuration.Bind("LetsEncryptSDK", options));
        }
    }
}
