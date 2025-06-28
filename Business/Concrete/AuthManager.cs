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
using AutoMapper;
using System.Security.Cryptography;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IMessageService _messages;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthManager(UserManager<ApplicationUser> userManager, IConfiguration config, IMessageService messages, IMapper mapper)
        {
            _userManager = userManager;
            _config = config;
            _messages = messages;
            _mapper = mapper;
        }

        public async Task<IResult> RegisterAsync(RegisterDto dto)
        {
            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return new ErrorResult(_messages["RegisterFailed"] + Environment.NewLine + string.Join(Environment.NewLine, result.Errors.Select(er => er.Description)));

            return new SuccessResult(_messages["RegisterSuccess"]);
        }

        public async Task<IDataResult<TokenResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return new ErrorDataResult<TokenResponseDto>(_messages["InvalidCredentials"]);

            var accessToken = GenerateJwt(user);
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(double.Parse(_config["Jwt:RefreshTokenDurationInDays"]));
            await _userManager.UpdateAsync(user);

            return new SuccessDataResult<TokenResponseDto>(new TokenResponseDto
            {
                AccessToken = accessToken.Data,
                RefreshToken = refreshToken
            });
        }
        public async Task<IDataResult<TokenResponseDto>> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null)
                return new ErrorDataResult<TokenResponseDto>(_messages["InvalidAccessToken"]);

            var userId = principal.FindFirstValue("id");
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null || user.RefreshToken != dto.RefreshToken)
                return new ErrorDataResult<TokenResponseDto>(_messages["InvalidRefreshToken"]);
            else if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return new ErrorDataResult<TokenResponseDto>(_messages["RefreshTokenExpired"]);
            var newAccessToken = GenerateJwt(user).Data;
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new SuccessDataResult<TokenResponseDto>(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
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

            return new SuccessDataResult<string>(data:new JwtSecurityTokenHandler().WriteToken(token));
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = false // <== This is key for expired tokens
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
                if (securityToken is not JwtSecurityToken jwtSecurityToken)
                    return null;

                return principal;
            }
            catch
            {
                return null;
            }
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
