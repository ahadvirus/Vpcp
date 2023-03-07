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

        if (result != null && result.IsGenericMethod)
        {
            result = result.MakeGenericMethod(genericTypes);
        }

        return result;
    }

    public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments,
        IDiagnosticsLogger<DbLoggerCategory.Query> logger)
    {
        MethodInfo? caseMethod = method.IsGenericMethod ? 
            Method(method.GetGenericArguments()) : 
            null;

        if (caseMethod == null || caseMethod != method)
        {
            return null;
        }

        return ExpressionFactory.Case(new List<CaseWhenClause>()
        {
            new CaseWhenClause( arguments.Skip(1).First(), arguments.Skip(2).First())
        }, arguments.LastOrDefault());
        
    }
}