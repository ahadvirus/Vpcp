using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vpcp.Kernel.Models.Contracts;

namespace Vpcp.Kernel.Models.Configurations;

public abstract class EntityConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity> where TKey : struct where TEntity : class, IEntity<TKey>
{
    protected DatabaseProvider DatabaseProvider { get; }
    protected MySqlConfiguration MySqlConfiguration { get; }

    protected EntityConfiguration(DatabaseProvider databaseProvider)
    {
        DatabaseProvider = databaseProvider;
        MySqlConfiguration = MySqlConfiguration.Instance();
    }

    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}