using BX.Business.Managers;
using BX.Models;
using BX.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace BX.Services.Authentication
{
    public interface IAuthenticationJwtService
    {
        Task<TokenResponseDto?> LoginAsync(LoginRequestDto request);
        //Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request);
        Task<User?> RegisterAsync(LoginRequestDto request);
    }

    public class AuthenticationJwtService : IAuthenticationJwtService
    {
        IUserManager _userManager;
        IConfiguration _configuration;

        public AuthenticationJwtService(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<TokenResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userManager.GetUserOnLogon(request);
            if (user is null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, request.Password)
                == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return await CreateTokenResponse(user);
        }

        private async Task<TokenResponseDto> CreateTokenResponse(User? user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshTokenAsync(user)
            };
        }

        public async Task<User?> RegisterAsync(LoginRequestDto request)
        {
            if (await _userManager.GetUserOnLogon(request) is null)
            {
                return null;
            }

            var user = new User();
            var hashedPassword = new PasswordHasher<User>()
                .HashPassword(user, request.Password);

            user.UserName = request.UserName;
            user.Password = hashedPassword;

            await _userManager.Create(user);
            return await _userManager.GetUserOnLogon(request);
        }

        //public async Task<TokenResponseDto?> RefreshTokensAsync(RefreshTokenRequestDto request)
        //{
        //    var user = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
        //    if (user is null)
        //        return null;

        //    return await CreateTokenResponse(user);
        //}

        //private async Task<User?> ValidateRefreshTokenAsync(string UserId, string refreshToken)
        //{
        //    var user = await _userManager.GetUserOnLogon(new User { UserId = UserId });
        //    if (user is null || user.RefreshToken != refreshToken
        //        || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        //    {
        //        return null;
        //    }
        //    return user;
        //}

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshTokenAsync(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.Update(user);
            return refreshToken;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Authentication:Token"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration["AppSettings:Issuer"],
                audience: _configuration["AppSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
