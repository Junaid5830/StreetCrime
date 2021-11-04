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
          Name = "Administrator",
          NormalizedName = "ADMINISTRATOR"
        },
        new IdentityRole
        {
          Id = "fc9fb251-1ea3-4048-883f-d4a203038aa8",
          Name = "Driver",
          NormalizedName = "DRIVER"
        },
         new IdentityRole
         {
           Id = "55bf1f9f-88fe-4a7c-9596-d97e9ce2b699",
           Name = "Company",
           NormalizedName = "COMPANY"
         });

    }
  }
}
