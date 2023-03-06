using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Vpcp.Kernel.Extensions.DbFunctions;

namespace Vpcp.Kernel.Databases.Functions;

public class CaseTranslator : IMethodCallTranslator
{
    private ISqlExpressionFactory ExpressionFactory { get; }

    public CaseTranslator(ISqlExpressionFactory expressionFactory)
    {
        ExpressionFactory = expressionFactory;
    }

    private MethodInfo? Method(Type[] genericTypes)
    {
        MethodInfo? result = typeof(CaseExtension)
            .GetMethod(nameof(CaseExtension.Case));

        if (result != null && result.IsGenericMethod && genericTypes.Length > 1)
        {
            result = result.MakeGenericMethod(genericTypes);
        }

        return result;
    }

    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        MethodInfo? caseMethod = method.IsGenericMethod && method.GetGenericArguments().Length > 1 ? 
            Method(method.GetGenericArguments()) : 
            null;

        if (caseMethod == null || caseMethod != method)
        {
            return null;
        }

        return SqlFunctionExpression.Create("MAX", arguments, method.ReturnType, null);
        
        /*
        return ExpressionFactory.Case(new List<CaseWhenClause>()
        {
            new CaseWhenClause( ExpressionFactory.Equal())
        }, arguments.LastOrDefault());
        */
    }
}