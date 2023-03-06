using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using Vpcp.Kernel.Models.Configurations;
using AdminEntity = Vpcp.Kernel.Models.Entities.Admin;

namespace Vpcp.Kernel.Databases.Configurations;

public class AdminConfiguration : EntityConfiguration<AdminEntity, Guid>
{
    public AdminConfiguration(DatabaseProvider database) : base(database)
    {
    }

    public override void Configure(EntityTypeBuilder<AdminEntity> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Key)
            .IsRequired();

        builder.Property(c => c.Value)
            .IsRequired();

        if (DatabaseProvider == DatabaseProvider.MySql)
        {
            builder.Property(c => c.Key)
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(c => c.Value)
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(c => c.Id)
                .HasConversion(value => value.ToString(), value => Guid.Parse(value))
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(c => c.UserId)
                .HasConversion(value => value.ToString(), value => Guid.Parse(value))
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4)
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder
                .ForMySQLHasCollation(MySqlConfiguration.Collation.Utf8Mb4General);
            
            /*
            builder
                .ForMySQLHasCharset(MySqlConfiguration.Charset.Utf8Mb4);
            */
        }

        builder.HasIndex(c => new { c.UserId, c.Key })
            .IsUnique();

        builder.ToTable("Admins");
    }
}