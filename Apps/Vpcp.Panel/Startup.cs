using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Vpcp.Panel;

internal static class Startup
{
    internal static void ConfigurationService(IServiceCollection services)
    {
        //
    }

    public static void Configuration(WebApplication app)
    {
        app.UseStaticFiles();
    }

    
}