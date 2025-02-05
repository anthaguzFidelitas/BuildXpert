using Microsoft.EntityFrameworkCore;

namespace BX.Models
{
    public interface IBuildXpertContext
    {
        DbSet<User> Users { get; set; }
        Task<bool> CanConnectAsync();
    }
}