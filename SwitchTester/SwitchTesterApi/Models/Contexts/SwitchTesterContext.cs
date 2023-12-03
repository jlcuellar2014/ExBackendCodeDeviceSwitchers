using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    /// <summary>
    /// Represents the implementation of the switch tester context for interacting with data related to switch testing.
    /// </summary>
    public class SwitchTesterContext(IConfiguration configuration) : DbContext, ISwitchTesterContext
    {
        /// <summary>
        /// Gets or sets the database set of switches.
        /// </summary>
        public DbSet<Switch> Switches { get; set; }

        /// <summary>
        /// Gets or sets the database set of devices.
        /// </summary>
        public DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the database set of switch ports.
        /// </summary>
        public DbSet<SwitchPort> SwitchPorts { get; set; }

        /// <summary>
        /// Gets or sets the database set of device ports.
        /// </summary>
        public DbSet<DevicePort> DevicePorts { get; set; }

        /// <summary>
        /// Gets or sets the database set of device-switch connections.
        /// </summary>
        public DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }

        /// <summary>
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
            modelBuilder.Entity<DeviceSwitchConnection>()
               .HasKey(o => new { o.DeviceId, o.SwitchId, o.Port });

            modelBuilder.Entity<DevicePort>()
                .HasKey(o => new { o.DeviceId, o.Port });

            modelBuilder.Entity<SwitchPort>()
                .HasKey(o => new { o.SwitchId, o.Port });
        }
    }
}
