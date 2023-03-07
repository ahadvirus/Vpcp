using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Vpcp.Kernel.Models.Databases;

namespace Vpcp.Kernel.Databases.Functions
{

    public class OperatorTranslator : IMethodCallTranslator
    {

        private ISqlExpressionFactory ExpressionFactory { get; }

        public OperatorTranslator(ISqlExpressionFactory expressionFactory)
        {
            ExpressionFactory = expressionFactory;
        }

        private IEnumerable<MethodInfo> Method(Type? genericTypes)
        {
            foreach (MethodInfo method in typeof(Operator).GetMethods())
            {
                if (method.IsGenericMethod && genericTypes != null)
                {
                    yield return method.MakeGenericMethod(genericTypes);
                }
            }

        }

        public SqlExpression? Translate(SqlExpression? instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments,
            IDiagnosticsLogger<DbLoggerCategory.Query> logger)
        {
            MethodInfo? operatorMethod = null;

            if (method.IsGenericMethod)
            {
                foreach (MethodInfo methodInfo in Method(method.GetGenericArguments().FirstOrDefault()))
                {
                    if (methodInfo == method)
                    {
                        operatorMethod = methodInfo;
                        break;
                    }
                }
            }

            if (operatorMethod == null)
            {
                return null;
            }

            MethodInfo? sqlOperatorMethod = typeof(ISqlExpressionFactory).GetMethod(operatorMethod.Name);

            if (sqlOperatorMethod == null)
            {
                return null;
            }

            return (SqlExpression?)sqlOperatorMethod.Invoke(ExpressionFactory,
                arguments.Select(e => (object)e).ToArray());
        }
    }
}
