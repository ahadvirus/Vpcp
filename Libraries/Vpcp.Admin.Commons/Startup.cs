using Microsoft.Extensions.DependencyInjection;
using Vpcp.Admin.Commons.Services;

namespace Vpcp.Admin.Commons
{
    public static class Startup
    {
        public static void ConfigurationService(IServiceCollection service)
        {

            service.AddScoped<AdminService>();
        }
    }
}
