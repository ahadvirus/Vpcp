using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Vpcp.Admin.Data.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            Commons.Database.MySqlConfiguration configuration = Commons.Database.MySqlConfiguration.Instance();

            builder.Property(t => t.UserId)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.Property(t => t.RoleId)
                .UseCollation(configuration.Collation.Utf8Mb4General)
                .HasCharSet(configuration.Charset.Utf8Mb4);

            builder.ToTable("UserRoles");
        }
    }
}
