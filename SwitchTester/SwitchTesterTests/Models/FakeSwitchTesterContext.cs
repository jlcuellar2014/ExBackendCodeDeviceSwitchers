using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    public class FakeSwitchTesterContext : DbContext, ISwitchTesterContext
    {
        public DbSet<Switch> Switches { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SwitchPort> SwitchPorts { get; set; }
        public DbSet<DevicePort> DevicePorts { get; set; }
        public DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }

        public async Task SaveChangesAsync() => await base.SaveChangesAsync();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseInMemoryDatabase("FakeDb");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceSwitchConnection>()
               .HasKey(o => new { o.DeviceId, o.SwitchId, o.Port });

            modelBuilder.Entity<DevicePort>()
                .HasKey(o => new { o.DeviceId, o.Port });

            modelBuilder.Entity<SwitchPort>()
                .HasKey(o => new { o.SwitchId, o.Port });
        }

        public override void Dispose()
        {
            this.Database.EnsureDeleted();
            base.Dispose();
        }
    }
}
