using BX.Data.Repository;
using BX.Models;
using BX.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Business.Managers
{
    public interface IUserManager
    {
        Task<User?> GetUserOnLogon(LoginRequestDto user);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
    }

    public class UserManager : IUserManager
    {
        IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User?> GetUserOnLogon(LoginRequestDto user)
        {
            User result = null;
            if (user is null) return null;
            if (user.UserName != null) result = await _userRepository.GetUserByUsernameAsync(user.UserName);
            if (user.Email != null) result = await _userRepository.GetUserByEmailAsync(user.Email);
            if (user.UserId != null) result = await _userRepository.GetByIdAsync(user.UserId);
            return result;
        }

        public async Task<bool> Create(User user)
        {
            return await _userRepository.CreateAsync(user);
        }
        public async Task<bool> Update(User user)
        {
            return await _userRepository.UpdateAsync(user);
        }
    }
}
