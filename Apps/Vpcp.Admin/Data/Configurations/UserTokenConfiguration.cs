using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Admin.Data.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            Commons.Database.MySqlConfiguration configuration = Commons.Database.MySqlConfiguration.Instance();

            builder.Property(t => t.UserId)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.LoginProvider)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.Name)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.Value)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.ToTable("UserTokens");
        }
    }
}
