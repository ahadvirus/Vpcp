using System;
using Vpcp.Kernel.Models.Databases;
using Vpcp.Kernel.Models.Enums;

namespace Vpcp.Kernel.Extensions.DbFunctions;

public static class CaseExtension
{
    public static TReturn Case<TReturn>(
        this Microsoft.EntityFrameworkCore.DbFunctions _, 
        Operator @operator,
        TReturn trueStatement,
        TReturn? falseStatement
    )
    {
        throw new InvalidOperationException(string.Empty);
    }
}