using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Vpcp.Kernel.Extensions.DbFunctions;

namespace Vpcp.Kernel.Databases.Functions;

public class MaxTranslator : IMethodCallTranslator
{
    
    private ISqlExpressionFactory ExpressionFactory { get; }
    
    public MaxTranslator(ISqlExpressionFactory expressionFactory)
    {
        ExpressionFactory = expressionFactory;
    }
    

    private MethodInfo? Method(Type? genericType)
    {
        MethodInfo? result = typeof(MaxExtension).GetMethod(
            nameof(MaxExtension.Max));

        if (result != null && result.IsGenericMethod && genericType != null)
        {
            result = result.MakeGenericMethod(genericType);
        }

        return result;
    }

    [Obsolete]
    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method,
        IReadOnlyList<SqlExpression> arguments,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        MethodInfo? maxMethod = method.IsGenericMethod ? Method(method.GetGenericArguments().FirstOrDefault()) : null;

        if (maxMethod == null || maxMethod != method)
        {
            return null;
        }

        SqlExpression[] args = new SqlExpression[] { arguments[1] };

        return ExpressionFactory.Function("MAX", args, method.ReturnType, null);
    }
}