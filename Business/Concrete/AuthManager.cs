using Business.Abstract;
using Entities.DTOs;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Localization;
using Core.Utilities.Result;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMessageService _messages;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration config, IMessageService messages)
        {
            _userManager = userManager;
            _config = config;
            _messages = messages;
        }

        public async Task<IResult> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.Email,
                FullName = dto.FullName,
                Gender = dto.Gender,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return new ErrorResult(_messages["RegisterFailed"] + Environment.NewLine + string.Join(Environment.NewLine,result.Errors.Select(er => er.Description)));

            return new SuccessResult(_messages["RegisterSuccess"]);
        }

        public async Task<IDataResult<string>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return new ErrorDataResult<string>(_messages["InvalidCredentials"]);

            return GenerateJwt(user);
        }

        private IDataResult<string> GenerateJwt(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );

            return new SuccessDataResult<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
