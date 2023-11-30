using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models
{
    public class SwitchTesterContext : DbContext, ISwitchTesterContext
    {
        public DbSet<Switch> Switches { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SwitchPort> SwitchPorts { get; set; }
        public DbSet<DevicePort> DevicePorts { get; set; }
        public DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //  I'm sure that is not the way to do it :)
            optionsBuilder.UseSqlite($"Data Source=SwitchTester.db");
        }

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
