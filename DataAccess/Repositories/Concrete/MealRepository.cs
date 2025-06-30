using DataAccess.DbContext.EntityFrameworkCore;
using DataAccess.Repositories.Abstract;
using Entities;

namespace DataAccess.Repositories.Concrete
{
    public class MealRepository : RepositoryBase<Meal>, IMealRepository
    {
        public MealRepository(ApplicationDbContext context) : base(context)
        {
        }
        public override async Task AddAsync(Meal meal)
        {
            foreach (var item in meal.MealItems)
            {
                item.Meal = meal;
            }

            await _context.Meals.AddAsync(meal);
        }
        public async Task<IEnumerable<Meal>> GetMealsInDay(int userId, DateTime date, bool trackChanges)
        {
            var meals = await GetAllAsync(m =>
                m.UserId == userId && (m.Date.Year == date.Year &&
                                       m.Date.Month == date.Month &&
                                       m.Date.Day == date.Day),
                trackChanges,
                "MealItems");

            return meals;
        }
        public async Task<IEnumerable<Meal>> GetMealsByDateAndTypeAsync(int userId, DateTime date, string mealType, bool trackChanges)
        {
            return await GetAllAsync(
                m => m.UserId == userId &&
                     m.MealType == mealType &&
                     (m.Date.Year == date.Year &&
                        m.Date.Month == date.Month &&
                        m.Date.Day == date.Day),
                trackChanges,
                "MealItems"
            );
        }

    }
}
