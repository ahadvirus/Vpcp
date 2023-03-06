using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Admin.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            Commons.Database.MySqlConfiguration configuration = Commons.Database.MySqlConfiguration.Instance();

            builder.Property(t => t.Id)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.UserName)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.NormalizedUserName)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.Email)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.NormalizedEmail)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.PasswordHash)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.SecurityStamp)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.ConcurrencyStamp)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.PhoneNumber)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.ToTable("Users");
        }
    }
}
