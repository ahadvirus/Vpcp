using Microsoft.Extensions.DependencyInjection;
using Vpcp.Panel.Api.Kernel.Databases.Contexts;

namespace Vpcp.Panel.Api.Kernel
{
    internal class Startup
    {
        internal static void ConfigurationService(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>();

            services.AddDbContext<MemoryContext>();
        }
    }
}
