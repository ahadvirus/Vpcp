using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Extensions;
using Vpcp.Kernel.Models.Entities;

namespace Vpcp.Kernel.Databases.Contexts;

public class KernelContext : DbContext
{
    public KernelContext(DbContextOptions<KernelContext> options) : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; }
    public DbSet<Dictionary> Dictionaries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
    }

    //[Obsolete]
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Database, GetType().Assembly);
        
        /*

        MethodInfo? maxFunction = typeof(EntityFrameworkFunctionsExtension)
            .GetRuntimeMethod(nameof(EntityFrameworkFunctionsExtension.Max), new[] { typeof(DbFunctions), typeof(object) });


        MethodInfo? caseFunction = typeof(EntityFrameworkFunctionsExtension)
            .GetRuntimeMethod(nameof(EntityFrameworkFunctionsExtension.Max), new[] { typeof(object), typeof(object), typeof(object), typeof(object) });

        if (maxFunction != null)
        {
            modelBuilder.HasDbFunction(maxFunction)
                .HasTranslation(args => SqlFunctionExpression.Create(
                    maxFunction.Name.ToUpper(),
                    args.Where(arg => arg.Type != typeof(DbFunctions)),
                    typeof(object),
                    null
                ));
        }
        
        */
    }
}