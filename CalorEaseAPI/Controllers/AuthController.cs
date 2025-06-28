using Business.Abstract;
using Core.Utilities.Result;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CalorEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var msg = await _authService.RegisterAsync(dto);
            if (!msg.Success)
                return BadRequest(msg.Message);

            return Ok(msg.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            return Ok(new { token });
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto tokenDto)
        {
            var result = await _authService.RefreshTokenAsync(tokenDto);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
    }
}
