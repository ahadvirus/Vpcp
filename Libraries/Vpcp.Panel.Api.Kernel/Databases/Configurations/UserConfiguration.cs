using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vpcp.Kernel.Extensions;
using Vpcp.Kernel.Models.Configurations;
using Vpcp.Panel.Api.Kernel.Models.Entities;

namespace Vpcp.Panel.Api.Kernel.Databases.Configurations;

public class UserConfiguration : EntityConfiguration<User, Guid>
{
    public UserConfiguration(DatabaseProvider databaseProvider) : base(databaseProvider)
    {
    }

    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Mobile)
            .IsRequired();

        builder.Property(t => t.Email)
            .IsRequired();

        builder.Property(t => t.Password)
            .IsRequired();

        builder.Property(t => t.Password)
            .IsRequired();

        if (DatabaseProvider == DatabaseProvider.MySql)
        {
            builder.Property(t => t.Mobile)
                .HasCharSet(MySqlConfiguration.Charset.Utf8Mb4)
                .HasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(t => t.Email)
                .HasCharSet(MySqlConfiguration.Charset.Utf8Mb4)
                .HasCollation(MySqlConfiguration.Collation.Utf8Mb4General);

            builder.Property(t => t.Username)
                .HasCharSet(MySqlConfiguration.Charset.Utf8Mb4)
                .HasCollation(MySqlConfiguration.Collation.Utf8Mb4General);


            builder.Property(t => t.Password)
                .HasCharSet(MySqlConfiguration.Charset.Utf8Mb4)
                .HasCollation(MySqlConfiguration.Collation.Utf8Mb4General);
        }

        builder.ToTable("Users");
    }
}