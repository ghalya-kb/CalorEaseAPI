using Core.Utilities.Result;
using Entities;
using Entities.DTOs;

namespace DataAccess.Repositories.Abstract
{
    public interface IMealRepository : IRepositoryBase<Meal>
    {
        new Task AddAsync(Meal meal);
        Task<IEnumerable<Meal>> GetMealsInDay(int userId, DateTime date, bool trackChanges);
        Task<IEnumerable<Meal>> GetMealsByDateAndTypeAsync(int userId, DateTime date, string mealType, bool trackChanges);

    }
}
