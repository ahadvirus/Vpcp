using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vpcp.Panel.Api.Kernel.Databases.Contexts;
using Vpcp.Panel.Api.Kernel.Models.Configurations;

namespace Vpcp.Panel.Api.Kernel.Extensions
{
    public static class DependencyExtensions
    {
        public static void AddPanelDependencies(this IServiceCollection services, Action<PanelConfiguration> options)
        {
            PanelConfiguration configuration = new PanelConfiguration();

            options.Invoke(configuration);

            services.AddScoped<DbContextOptions<IdentityContext>>(_ => configuration.IdentityOptions.Options);

            services.AddScoped<DbContextOptions<MemoryContext>>(_ => configuration.MemoryOptions.Options);

            Startup.ConfigurationService(services);
        }
    }
}
