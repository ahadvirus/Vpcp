using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Vpcp.Kernel.Extensions;

public static class MigrationExtension
{
    public static async void RunDatabasesMigrations(this IApplicationBuilder app)
    {
        ConsoleColor defaultForegroundColor = Console.ForegroundColor;

        try
        {
            using (IServiceScope scope = app.ApplicationServices.CreateAsyncScope())
            {
                IEnumerable<Type> contexts = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.IsClass && type.BaseType != null && type.BaseType == typeof(DbContext));


                foreach (Type type in contexts)
                {
                    object? context = scope.ServiceProvider.GetService(type);

                    if (context == null)
                    {
                        continue;
                    }

                    try
                    {
                        string? providerName = ((DbContext)context).Database.ProviderName;

                        if (
                            !string.IsNullOrEmpty(providerName) &&
                            !providerName.Contains(nameof(Microsoft.EntityFrameworkCore.InMemory))
                        )
                        {
                            await ((DbContext)context).Database.MigrateAsync();
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
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(e);
            Console.ForegroundColor = defaultForegroundColor;
        }
    }
}