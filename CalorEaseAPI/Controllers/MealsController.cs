using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CalorEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MealsController : ControllerBase
    {
        private readonly IMealService _mealService;

        public MealsController(IMealService mealService)
        {
            _mealService = mealService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddMeal([FromBody] MealDto dto)
        {
            var userId = int.Parse(User.FindFirstValue("id"));
            var result = await _mealService.AddMealAsync(userId, dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpDelete("{mealId}")]
        public async Task<IActionResult> DeleteMeal(int mealId)
        {
            var userId = int.Parse(User.FindFirstValue("id"));
            var result = await _mealService.DeleteMealAsync(userId, mealId);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpGet("daily")]
        public async Task<IActionResult> GetDailyMeals()
        {
            var userId = int.Parse(User.FindFirstValue("id"));

            var result = await _mealService.GetDailyMealsAsync(userId);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }
        [HttpGet("by-type")]
        public async Task<IActionResult> GetByDateAndType([FromQuery] GetMealsByTypeRequestDto dto)
        {
            var userId = int.Parse(User.FindFirst("id")!.Value);
            var result = await _mealService.GetMealsByDateAndTypeAsync(userId, dto.MealType);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
