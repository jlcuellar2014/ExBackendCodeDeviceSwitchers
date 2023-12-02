using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SwitchTesterApi.Models.Contexts
{
    public class SwitchTesterContext(IConfiguration configuration) : DbContext, ISwitchTesterContext
    {
        public DbSet<Switch> Switches { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SwitchPort> SwitchPorts { get; set; }
        public DbSet<DevicePort> DevicePorts { get; set; }
        public DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseSqlite(configuration.GetConnectionString("sqlite"));

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
