using Microsoft.EntityFrameworkCore;

namespace SwitchTesterApi.Models.Contexts
{
    public interface ISegurityContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        Task SaveChangesAsync();
    }
}