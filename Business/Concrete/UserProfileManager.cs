using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Repositories.Abstract;
using Entities.DTOs;
using Entities;
using Microsoft.Extensions.Localization;
using Business.Localization;

namespace Business.Concrete
{
    public class UserProfileManager : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMessageService _messages;

        public UserProfileManager(
            IUserProfileRepository userProfileRepository,
            IMessageService messages)
        {
            _userProfileRepository = userProfileRepository;
            _messages = messages;
        }

        public async Task<IResult> CreateAsync(int userId, UserProfileDto dto)
        {
            var existingProfile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: false);
            if (existingProfile != null)
            {
                return new ErrorResult(_messages["UserProfileAlreadyExists"]);
            }

            var profile = new UserProfile
            {
                UserId = userId,
                HeightCm = dto.HeightCm,
                WeightKg = dto.WeightKg,
                Age = dto.Age,
                ActivityLevel = dto.ActivityLevel,
                GoalType = dto.GoalType,
                BMR = dto.BMR,
                TDEE = dto.TDEE,
                CalorieTarget = dto.CalorieTarget
            };

            await _userProfileRepository.AddAsync(profile);
            await _userProfileRepository.SaveAsync();

            return new SuccessResult(_messages["UserProfileCreated"]);
        }

        public async Task<IResult> UpdateAsync(int userId, UserProfileDto dto)
        {
            var profile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: true);
            if (profile == null)
            {
                return new ErrorResult(_messages["UserProfileNotFound"]);
            }

            profile.HeightCm = dto.HeightCm;
            profile.WeightKg = dto.WeightKg;
            profile.Age = dto.Age;
            profile.ActivityLevel = dto.ActivityLevel;
            profile.GoalType = dto.GoalType;
            profile.BMR = dto.BMR;
            profile.TDEE = dto.TDEE;
            profile.CalorieTarget = dto.CalorieTarget;

            _userProfileRepository.Update(profile);
            await _userProfileRepository.SaveAsync();
            return new SuccessResult(_messages["UserProfileUpdated"]);
        }

        public async Task<IDataResult<UserProfileDto>> GetByUserIdAsync(int userId)
        {
            var profile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: false);
            if (profile == null)
            {
                return new ErrorDataResult<UserProfileDto>(_messages["UserProfileNotFound"]);
            }

            var dto = new UserProfileDto
            {
                HeightCm = profile.HeightCm,
                WeightKg = profile.WeightKg,
                Age = profile.Age,
                ActivityLevel = profile.ActivityLevel,
                GoalType = profile.GoalType,
                BMR = profile.BMR,
                TDEE = profile.TDEE,
                CalorieTarget = profile.CalorieTarget
            };

            return new SuccessDataResult<UserProfileDto>(dto, _messages["UserProfileFetched"]);
        }
    }
}
