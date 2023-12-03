using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    /// <summary>
    /// Interface representing the context for interacting with data related to switch testing.
    /// </summary>
    public interface ISwitchTesterContext
    {
        /// <summary>
        /// Gets or sets the database set of device ports.
        /// </summary>
        DbSet<DevicePort> DevicePorts { get; set; }

        /// <summary>
        /// Gets or sets the database set of devices.
        /// </summary>
        DbSet<Device> Devices { get; set; }

        /// <summary>
        /// Gets or sets the database set of device-switch connections.
        /// </summary>
        DbSet<DeviceSwitchConnection> DeviceSwitchConnections { get; set; }

        /// <summary>
        /// Gets or sets the database set of switches.
        /// </summary>
        DbSet<Switch> Switches { get; set; }

        /// <summary>
        /// Gets or sets the database set of switch ports.
        /// </summary>
        DbSet<SwitchPort> SwitchPorts { get; set; }

        /// <summary>
        /// Synchronously saves changes made to the context to the underlying database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Asynchronously saves changes made to the context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task SaveChangesAsync();
    }
}