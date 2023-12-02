using Microsoft.EntityFrameworkCore;
using SwitchTesterApi.Models;
using SwitchTesterApi.Models.Contexts;

namespace SwitchTesterTests.Models
{
    public class FakeSecurityContext : DbContext, ISecurityContext
    {
        public DbSet<User> ApplicationUsers { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FakeDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasKey(o => new { o.UserName });
        }

        public override void Dispose()
        {
            this.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
 