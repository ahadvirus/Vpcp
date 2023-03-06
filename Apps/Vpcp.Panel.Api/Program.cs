using Microsoft.AspNetCore.Builder;
using Vpcp.Kernel.Extensions;

namespace Vpcp.Panel.Api;

public static class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        Startup.ConfigurationService(builder.Services);

        WebApplication app = builder.Build();

        Startup.Configuration(app);

        app.RunDatabasesMigrations();

        //app.RunDatabasesSeeds();

        app.Run();
    }
}