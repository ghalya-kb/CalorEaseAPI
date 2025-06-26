using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace CalorEaseAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        private readonly IStringLocalizer<UserProfileController> _localizer;

        public UserProfileController(IUserProfileService userProfileService, IStringLocalizer<UserProfileController> localizer)
        {
            _userProfileService = userProfileService;
            _localizer = localizer;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserProfileDto dto)
        {
            int userId = GetUserId();
            var result = await _userProfileService.CreateAsync(userId, dto);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserProfileDto dto)
        {
            int userId = GetUserId();
            var result = await _userProfileService.UpdateAsync(userId, dto);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get()
        {
            int userId = GetUserId();
            var result = await _userProfileService.GetByUserIdAsync(userId);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue("id")!);
        }
    }
}
