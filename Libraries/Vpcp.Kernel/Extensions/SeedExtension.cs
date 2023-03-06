using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Vpcp.Kernel.Databases.Repositories.Persistence;
using Vpcp.Kernel.Databases.Repositories.Presentations;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Extensions;

public static class SeedExtension
{
    public static async void RunDatabasesSeeds(this IApplicationBuilder app)
    {
        ConsoleColor defaultForegroundColor = Console.ForegroundColor;

        try
        {
            using (IServiceScope scope = app.ApplicationServices.CreateAsyncScope())
            {
                /*
                IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsClass && type.BaseType != null && type.BaseType == typeof(DbContext));

                List<DbContext> contexts = new List<DbContext>();

                foreach (Type type in types)
                {
                    object? context = scope.ServiceProvider.GetService(type);

                    if (context == null)
                    {
                        continue;
                    }

                    contexts.Add((DbContext)context);

                }
                */

                IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type =>
                        type.IsClass &&
                        type.GetInterfaces().Any(@interface =>
                            @interface.IsGenericType &&
                            @interface.GetGenericTypeDefinition() == typeof(ISeed<,>)
                            )
                    );
                
                Type repository = typeof(Repository<,>);

                string? methodName = typeof(ISeed<,>).GetMethods().Select(method => method.Name).FirstOrDefault();

                if (string.IsNullOrEmpty(methodName))
                {
                    throw new Exception(string.Empty);
                }

                foreach (Type type in types)
                {
                    if (type.GetConstructors().Any())
                    {
                        continue;
                    }
                    
                    MethodInfo? method = type.GetMethod(methodName);

                    if (method == null)
                    {
                        continue;
                    }

                    ParameterInfo? parameter = method.GetParameters().FirstOrDefault();

                    if (parameter == null)
                    {
                        continue;
                    }

                    if (!parameter.ParameterType.IsGenericType &&
                        parameter.ParameterType.GetGenericTypeDefinition() != typeof(IRepository<,>))
                    {
                        continue;
                    }

                    Type[] generics = parameter.ParameterType.GetGenericArguments();

                    if (generics.Length == 0)
                    {
                        continue;
                    }

                    repository = repository.MakeGenericType(generics);

                    ConstructorInfo? constructor = repository.GetConstructors().FirstOrDefault(
                        constructor => 
                            constructor.GetParameters().Any() && 
                            constructor.GetParameters().Length == 2
                            );

                    object? seed = Activator.CreateInstance(type);

                    if (seed == null)
                    {
                        continue;
                    }
                }

                
            }
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ForegroundColor = defaultForegroundColor;
        }
    }
}