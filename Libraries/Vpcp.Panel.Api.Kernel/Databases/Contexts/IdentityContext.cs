using Microsoft.EntityFrameworkCore;
using Vpcp.Kernel.Extensions;
using Vpcp.Panel.Api.Kernel.Models.Entities;

namespace Vpcp.Panel.Api.Kernel.Databases.Contexts;

public class IdentityContext : DbContext
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    //public DbSet<AdminDTO> Admins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.AddCustomExtension();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Database, GetType().Assembly);
    }
}