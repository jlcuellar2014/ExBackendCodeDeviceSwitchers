using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    /// <summary>
    /// Represents the implementation of the security context for interacting with user-related data.
    /// </summary>
    public class SecurityContext(IConfiguration configuration) : DbContext, ISecurityContext
    {
        /// <summary>
        /// Gets or sets the database set of application users.
        /// </summary>
        public DbSet<User> ApplicationUsers { get; set; }

        // <summary>
        /// Asynchronously saves changes made to the context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        /// <summary>
        /// Configures the context to connect to the SQLite database using the specified configuration.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite(configuration.GetConnectionString("sqlite"));

        /// <summary>
        /// Configures the model for the context during model creation.
        /// </summary>
        /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasKey(o => new { o.UserName });
        }
    }
}
