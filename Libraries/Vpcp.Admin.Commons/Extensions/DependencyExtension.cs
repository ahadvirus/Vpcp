using Microsoft.Extensions.DependencyInjection;

namespace Vpcp.Admin.Commons.Extensions
{
    public static class DependencyExtension
    {
        public static void AddAdminDependency(this IServiceCollection service)
        {
            Startup.ConfigurationService(service);
        }
    }
}
