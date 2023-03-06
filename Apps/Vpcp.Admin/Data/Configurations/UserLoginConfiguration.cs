using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Admin.Data.Configurations
{
    public class UserLoginConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            Commons.Database.MySqlConfiguration configuration = Commons.Database.MySqlConfiguration.Instance();

            builder.Property(t => t.LoginProvider)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.ProviderKey)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.ProviderDisplayName)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.UserId)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.ToTable("UserLogins");
        }
    }
}
