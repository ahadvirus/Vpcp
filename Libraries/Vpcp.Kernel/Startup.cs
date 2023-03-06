using Microsoft.Extensions.DependencyInjection;
using Vpcp.Kernel.Databases.Contexts;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Databases.Repositories.Presentations;

namespace Vpcp.Kernel;

public static class Startup
{
    public static void ConfigurationService(IServiceCollection service)
    {
        service.AddDbContext<KernelContext>();

        service.AddScoped<IAdminRepository>(provider =>
        {
            KernelContext context = provider.GetRequiredService<KernelContext>();

            return new AdminRepository(context.SaveChangesAsync, context.Admins);
        });
    }
}