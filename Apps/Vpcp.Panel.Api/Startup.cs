using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Extensions;
using Vpcp.Panel.Api.Kernel.Models.Configurations;
using Vpcp.Panel.Api.Kernel.Databases.Contexts;
using Vpcp.Panel.Api.Kernel.Extensions;

namespace Vpcp.Panel.Api;

internal static class Startup
{
    internal static void ConfigurationService(IServiceCollection services)
    {
        SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "(localdb)\\MSSQLLocalDB",
            InitialCatalog = "Vpcp",
            IntegratedSecurity = true
        };

        services.AddKernelDependencies(options =>
            options.KernelContextOptions.UseMySQL(
                MySqlConfiguration.Instance().ConnectionString,
                mySqlOption => mySqlOption.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name)
            )
        );

        services.AddPanelDependencies(options =>
        {
            options.IdentityOptions.UseMySQL(
                MySqlConfiguration.Instance().ConnectionString,
                mySqlOption => mySqlOption.MigrationsAssembly(typeof(IdentityContext).Assembly.GetName().Name)
            );

            options.MemoryOptions.UseInMemoryDatabase(nameof(MemoryContext));
        });

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    internal static void Configuration(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}