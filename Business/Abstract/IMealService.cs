using Core.Utilities.Result;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMealService
    {
        Task<IResult> AddMealAsync(int userId, MealDto dto);
        Task<IResult> DeleteMealAsync(int userId, int mealId);
        Task<IDataResult<IEnumerable<MealDto>>> GetDailyMealsAsync(int userId);
        Task<IDataResult<IEnumerable<MealDto>>> GetMealsByDateAndTypeAsync(int userId, string mealType);
    }
}
