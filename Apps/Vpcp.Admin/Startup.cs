using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Vpcp.Admin.Areas.Identity;
using Vpcp.Admin.Commons.Extensions;
using Vpcp.Admin.Data;
using Vpcp.Kernel.Extensions;

namespace Vpcp.Admin
{
    public static class Startup
    {
        public static void ConfigurationService(IServiceCollection service, ConfigurationManager configuration)
        {
            //string connectionString = configuration.GetConnectionString("DefaultConnection");
            string connectionString = Commons.Models.Configurations.MySqlConfiguration.Instance().ConnectionString;

            service.AddDbContext<IdentityContext>(options =>
                options.UseMySQL(connectionString));
            service.AddDatabaseDeveloperPageExceptionFilter();
            service.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<IdentityContext>();
            service.AddRazorPages();
            service.AddServerSideBlazor();
            service.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            service.AddKernelDependencies(options =>
            {
                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder()
                {
                    DataSource = "(localdb)\\MSSQLLocalDB",
                    InitialCatalog = "Vpcp",
                    IntegratedSecurity = true,
                };

                //connectionString = connectionStringBuilder.ConnectionString;
                /*
                string? assemblyName = typeof(AdminRepo).Assembly.GetName().Name;
                options.AssemblyName = string.IsNullOrEmpty(assemblyName) ? string.Empty : assemblyName;
                */
                //options.KernelContextOptions.UseSqlServer(connectionStringBuilder.ConnectionString, sqlServerOptions => sqlServerOptions.MigrationsAssembly("Ahad"));
                options.KernelContextOptions.UseMySQL(connectionStringBuilder.ConnectionString, mySqlOptions =>
                {
                    mySqlOptions.MigrationsAssembly(typeof(Startup).Assembly.GetName().Name);
                });
                return options;
            });
            
            service.AddAdminDependency();
            
            service.AddRouting(config =>
            {
                config.LowercaseUrls = true;
                config.LowercaseQueryStrings = true;
            });
        }

        public static void Configuration(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");
        }
    }
}
