using System;

namespace Vpcp.Kernel.Extensions.DbFunctions;

public static class MaxExtension
{
    public static T Max<T>(this Microsoft.EntityFrameworkCore.DbFunctions _, T args)
    {
        throw new InvalidOperationException(string.Empty);
    }
    
}