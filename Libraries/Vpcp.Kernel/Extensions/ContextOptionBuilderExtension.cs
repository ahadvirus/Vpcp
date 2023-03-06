using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vpcp.Kernel.Databases.Extensions;

namespace Vpcp.Kernel.Extensions;

public static class ContextOptionBuilderExtension
{
    public static DbContextOptionsBuilder AddCustomExtension(this DbContextOptionsBuilder entry)
    {
        // if the extension is registered already then we keep it
        // otherwise we create a new one
        ContextExtension extension = entry.Options
                                         .FindExtension<ContextExtension>()
                                     ?? new ContextExtension();
        ((IDbContextOptionsBuilderInfrastructure)entry).AddOrUpdateExtension(extension);

        return entry;
    }
}