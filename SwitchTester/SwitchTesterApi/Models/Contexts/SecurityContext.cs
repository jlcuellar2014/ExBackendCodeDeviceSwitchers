using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    public class SecurityContext : DbContext, ISecurityContext
    {
        private readonly IConfiguration configuration;

        public SecurityContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<User> ApplicationUsers { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(configuration.GetConnectionString("sqlite"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasKey(o => new { o.UserName });
        }
    }
}
