using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Vpcp.Kernel.Models.Configurations;

namespace Vpcp.Kernel.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder,
        DatabaseFacade database,
        Assembly assembly
    )
    {
        //string assemblyName = GetType().Assembly.GetName().FullName;


        string? databaseProvider = database.ProviderName?.Split('.').FirstOrDefault();

        if (!string.IsNullOrEmpty(databaseProvider))
        {
            /*
            IEnumerable<Type> configurations = GetType().Assembly
                .GetTypes()
                .Where(type => type.IsClass &&
                               type.GetInterfaces()
                                   .Any(@interface =>
                                       @interface.IsGenericType && @interface.GetGenericTypeDefinition() ==
                                       typeof(IEntityTypeConfiguration<>)));
            */
            //.Where(type => !string.IsNullOrEmpty(type.Namespace) && type.Namespace.Contains(database));

            IEnumerable<Type> configurations = assembly
                .GetTypes()
                .Where(type => type.IsClass &&
                               type.BaseType != null &&
                               type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(EntityConfiguration<,>));

            MethodInfo? applyConfiguration =
                typeof(ModelBuilder).GetMethod(nameof(ModelBuilder.ApplyConfiguration));

            /*

            DatabaseProvider? parameterValue = string.Join(",", Enum.GetValues(typeof(DatabaseProvider))).Split(',')
                .Select(value => value.Trim())
                .Where(value =>
                    string.Compare(value.ToLower(), database.ToLower(),
                        StringComparison.CurrentCultureIgnoreCase) == 0)
                .Select(Enum.Parse<DatabaseProvider>)
                .FirstOrDefault();
            */

            DatabaseProvider parameterValue = (databaseProvider.ToLower()) switch
            {
                "mysql" => DatabaseProvider.MySql,
                _ => DatabaseProvider.SqlServer
            };

            if (applyConfiguration != null && applyConfiguration.IsGenericMethod)
            {
                foreach (Type configuration in configurations)
                {
                    ConstructorInfo? constructor = configuration
                        .GetConstructors()
                        .FirstOrDefault(method =>
                            method.GetParameters().Length == 1 &&
                            method.GetParameters().First().ParameterType == typeof(DatabaseProvider)
                        );

                    if (constructor == null)
                    {
                        continue;
                    }

                    object instance = constructor.Invoke(new object[] { parameterValue });

                    Type? baseType = configuration.BaseType;

                    if (baseType != null && baseType.IsGenericType)
                    {
                        Type? genericType = baseType.GetGenericArguments().FirstOrDefault();

                        if (genericType != null)
                        {
                            MethodInfo generic = applyConfiguration.MakeGenericMethod(genericType);

                            generic.Invoke(modelBuilder, new[] { instance });
                        }
                    }
                }

            }

            /*
            modelBuilder.ApplyConfigurationsFromAssembly(
                AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .First(assembly => string.Compare(assembly.GetName().Name, _configuration.AssemblyName,
                        StringComparison.CurrentCultureIgnoreCase) == 0)
            );
            */
        }
    }
}
