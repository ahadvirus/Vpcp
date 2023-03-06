using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Vpcp.Kernel.Databases.Extensions.Infrastructures;

internal sealed class ContextInformation : DbContextOptionsExtensionInfo
{
    public ContextInformation(IDbContextOptionsExtension extension) : base(extension)
    {
    }

    public override int GetServiceProviderHashCode()
    {
        return 0;
    }

    public override bool ShouldUseSameServiceProvider(DbContextOptionsExtensionInfo other)
    {
        return false;
    }

    public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
    {
    }

    public override bool IsDatabaseProvider
    {
        get
        {
            return false;
        }
    }

    public override string LogFragment
    {
        get
        {
            return string.Empty;
        }
    }
}