using BX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Data.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> CreateAsync(User user);
        Task<User?> GetByIdAsync(string userId);
        Task<bool> UpdateAsync(User user);
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            //return await _context.Users
            //    .Include(u => u.Emails)
            //    .FirstOrDefaultAsync(u => u.Emails.Any(e => e.Email == email));
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

    }

}

