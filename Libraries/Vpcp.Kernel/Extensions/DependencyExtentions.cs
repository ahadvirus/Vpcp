using System;
using Microsoft.Extensions.DependencyInjection;
using Vpcp.Kernel.Models.Configurations;

namespace Vpcp.Kernel.Extensions;

public static class DependencyExtensions
{
    public static void AddKernelDependencies(this IServiceCollection service, Action<KernelConfiguration> options)
    {
        KernelConfiguration configuration = new KernelConfiguration();

        options.Invoke(configuration);

        /*

        RelationalOptionsExtension? relationalOptions = (RelationalOptionsExtension?) configuration.KernelContextOptions.Options.Extensions.FirstOrDefault(extension =>
            extension.GetType().BaseType != null && extension.GetType().BaseType == typeof(RelationalOptionsExtension));
        
        if (relationalOptions != null)
        {
            relationalOptions = relationalOptions.WithMigrationsAssembly(typeof(DependencyExtensions).Assembly.GetName().Name);

            ((IDbContextOptionsBuilderInfrastructure)configuration.KernelContextOptions).AddOrUpdateExtension(relationalOptions);
        }

        */

        service.AddScoped(_ => configuration.KernelContextOptions.Options);

        Startup.ConfigurationService(service);
    }
}