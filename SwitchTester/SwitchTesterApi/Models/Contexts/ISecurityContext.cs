using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    public interface ISecurityContext
    {
        DbSet<User> ApplicationUsers { get; set; }

        Task SaveChangesAsync();
    }
}