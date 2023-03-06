using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Admin.Data.Configurations
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            Commons.Database.MySqlConfiguration configuration = Commons.Database.MySqlConfiguration.Instance();

            builder.Property(t => t.UserId)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.ClaimType)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.ClaimValue)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.ToTable("UserClaims");
        }
    }
}
