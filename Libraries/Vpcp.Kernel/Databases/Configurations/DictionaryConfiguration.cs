using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using Vpcp.Kernel.Models.Configurations;
using DictionaryEntity = Vpcp.Kernel.Models.Entities.Dictionary;

namespace Vpcp.Kernel.Databases.Configurations;

public class DictionaryConfiguration : EntityConfiguration<DictionaryEntity, Guid>
{
    public DictionaryConfiguration(DatabaseProvider database) : base(database)
    {
    }

    public override void Configure(EntityTypeBuilder<DictionaryEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Word)
            .IsUnique();

        builder.Property(c => c.Active)
            .IsRequired();

        if (DatabaseProvider == DatabaseProvider.MySql)
        {
            builder.Property(c => c.Word)
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(c => c.Id)
                .HasConversion(value => value.ToString(), value => Guid.Parse(value))
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            /*
            builder
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4);
            */
            
            builder
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);
        }

        builder.ToTable("Dictionaries");
    }
}