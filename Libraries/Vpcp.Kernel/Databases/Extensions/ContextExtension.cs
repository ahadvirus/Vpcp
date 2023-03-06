using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;
using Vpcp.Kernel.Databases.Extensions.Infrastructures;
using Vpcp.Kernel.Databases.Plugins;

namespace Vpcp.Kernel.Databases.Extensions;

public class ContextExtension : IDbContextOptionsExtension
{
    private DbContextOptionsExtensionInfo? _info;
    public void ApplyServices(IServiceCollection services)
    {
        services.AddScoped<IMethodCallTranslatorProvider, MethodTranslatorPlugin>();
    }

    public void Validate(IDbContextOptions options)
    {
    }

    public DbContextOptionsExtensionInfo Info
    {
        get
        {
            return _info ??= new ContextInformation(this);
        }
    }
}