using BX.Models.Dto;
using BX.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BX.Services.Authentication;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationJwtService _authService;
        public AuthenticationController(IAuthenticationJwtService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(LoginRequestDto request)
        {
            User user = await _authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }

        //[HttpPost("refresh-token")]
        //public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
        //{
        //    var result = await _authService.RefreshTokensAsync(request);
        //    if (result is null || result.AccessToken is null || result.RefreshToken is null)
        //        return Unauthorized("Invalid refresh token.");

        //    return Ok(result);
        //}

        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            return Ok("You are authenticated!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            return Ok("You are and admin!");
        }
    }

}
