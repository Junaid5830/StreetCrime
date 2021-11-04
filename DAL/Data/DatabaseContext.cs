using DAL.Configurations.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
  public class DatabaseContext : IdentityDbContext<User>
  {
    public DatabaseContext(DbContextOptions options) : base(options)
    {

    }

    //public virtual DbSet<Card> Cards { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RoleConfiguration());
		}
  }
}
