using System;
using Vpcp.Kernel.Models.Enums;

namespace Vpcp.Kernel.Extensions.DbFunctions;

public static class CaseExtension
{
    public static TReturn Case<TProperty, TCondition, TReturn>(
        this Microsoft.EntityFrameworkCore.DbFunctions _, 
        TProperty property,
        QueryOperator @operator,
        TCondition condition,
        TReturn trueStatement, 
        TReturn? falseStatement
    )
    {
        throw new InvalidOperationException(string.Empty);
    }
}