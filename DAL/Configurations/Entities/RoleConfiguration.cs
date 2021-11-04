using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Configurations.Entities
{
  public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
  {
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
      builder.HasData(
        new IdentityRole
        {
          Id = "55bf8da2-b1c8-4bdb-933c-566d16676b77",
          Name = "User",
          NormalizedName = "USER"
        },
        new IdentityRole
        {
          Id = "18f4f705-bfbe-46dc-b2a1-2e89862c1e7f",
          Name = "Admin",
          NormalizedName = "ADMIN"
        });

    }
  }
}
