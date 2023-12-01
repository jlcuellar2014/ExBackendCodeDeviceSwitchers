using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    public class SegurityContext : DbContext, ISegurityContext
    {
        private readonly IConfiguration configuration;

        public SegurityContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(configuration.GetConnectionString("sqlite"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
               .HasKey(o => new { o.UserName });
        }
    }
}
