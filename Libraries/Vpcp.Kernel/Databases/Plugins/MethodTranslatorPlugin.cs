using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;
using Vpcp.Kernel.Databases.Functions;

namespace Vpcp.Kernel.Databases.Plugins;

public sealed class MethodTranslatorPlugin : RelationalMethodCallTranslatorProvider
{
    //public IEnumerable<IMethodCallTranslator> Translators { get; }
    /*
    private RelationalMethodCallTranslatorProviderDependencies Dependencies { get; }
    */
    private ISqlExpressionFactory ExpressionFactory { get; }
    

    public MethodTranslatorPlugin(RelationalMethodCallTranslatorProviderDependencies dependencies) : base(dependencies)
    {
        //Translators = translators;
        //Dependencies = dependencies;

        ExpressionFactory = dependencies.SqlExpressionFactory;
        
        AddTranslators( new List<IMethodCallTranslator>()
        {
            
            new CaseTranslator(ExpressionFactory),
            new MaxTranslator(ExpressionFactory),
            new OperatorTranslator(ExpressionFactory)
        });
    }
}