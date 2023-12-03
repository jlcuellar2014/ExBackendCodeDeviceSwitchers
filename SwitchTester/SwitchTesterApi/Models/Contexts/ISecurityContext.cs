using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    /// <summary>
    /// Interface representing the security context for interacting with user-related data.
    /// </summary>
    public interface ISecurityContext
    {
        /// <summary>
        /// Gets or sets the database set of application users.
        /// </summary>
        DbSet<User> ApplicationUsers { get; set; }

        /// <summary>
        /// Asynchronously saves changes made to the context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        Task SaveChangesAsync();
    }
}