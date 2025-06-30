using AutoMapper;
using Business.Abstract;
using Business.Localization;
using Core.Utilities.Result;
using DataAccess.Repositories.Abstract;
using Entities.DTOs;
using Entities;

namespace Business.Concrete
{
    public class MealManager : IMealService
    {
        private readonly IMealRepository _mealRepo;
        private readonly IMapper _mapper;
        private readonly IMessageService _messages;

        public MealManager(IMealRepository mealRepo, IMapper mapper, IMessageService messages)
        {
            _mealRepo = mealRepo;
            _mapper = mapper;
            _messages = messages;
        }

        public async Task<IResult> AddMealAsync(int userId, MealDto dto)
        {
            dto.Date = DateTime.UtcNow;
            var meal = _mapper.Map<Meal>(dto);
            meal.UserId = userId;
            meal.CreatedAt = DateTime.UtcNow;

            await _mealRepo.AddAsync(meal);
            await _mealRepo.SaveAsync();

            return new SuccessResult(_messages["MealAdded"]);
        }

        public async Task<IResult> DeleteMealAsync(int userId, int mealId)
        {
            var meal = await _mealRepo.FirstOrDefaultAsync(m => m.Id == mealId && m.UserId == userId, true);
            if (meal == null)
                return new ErrorResult(_messages["MealNotFound"]);

            _mealRepo.Remove(meal);
            await _mealRepo.SaveAsync();
            return new SuccessResult(_messages["MealDeleted"]);
        }

        public async Task<IDataResult<IEnumerable<MealDto>>> GetDailyMealsAsync(int userId)
        {
            var date = DateTime.UtcNow.Date;

            var meals = await _mealRepo.GetMealsInDay(userId, date, trackChanges: false);
            var result = _mapper.Map<IEnumerable<MealDto>>(meals);

            return new SuccessDataResult<IEnumerable<MealDto>>(result, _messages["DailyMealsFetched"]);
        }

        public async Task<IDataResult<IEnumerable<MealDto>>> GetMealsByDateAndTypeAsync(int userId, string mealType)
        {
            var date = DateTime.UtcNow.Date;
            var meals = await _mealRepo.GetMealsByDateAndTypeAsync(userId, date, mealType, trackChanges: false);

            var dto = _mapper.Map<IEnumerable<MealDto>>(meals);

            return new SuccessDataResult<IEnumerable<MealDto>>(dto, _messages["DailyMealsFetched"]);
        }
    }
}
