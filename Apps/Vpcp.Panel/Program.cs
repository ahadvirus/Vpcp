using Microsoft.AspNetCore.Builder;

namespace Vpcp.Panel;

public static class Program
{
    public static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        Startup.ConfigurationService(builder.Services);
        
        WebApplication app = builder.Build();
        
        Startup.Configuration(app);

        app.Run();


    }
}