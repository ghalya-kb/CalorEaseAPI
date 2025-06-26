using Business.Abstract;
using Core.Utilities.Result;
using DataAccess.Repositories.Abstract;
using Entities.DTOs;
using Entities;
using Microsoft.Extensions.Localization;
using Business.Localization;
using AutoMapper;

namespace Business.Concrete
{
    public class UserProfileManager : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMessageService _messages;
        private readonly IMapper _mapper;

        public UserProfileManager(
            IUserProfileRepository userProfileRepository,
            IMessageService messages,
            IMapper mapper)
        {
            _userProfileRepository = userProfileRepository;
            _messages = messages;
            _mapper = mapper;
        }

        public async Task<IResult> CreateAsync(int userId, UserProfileDto dto)
        {
            var existingProfile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: false);
            if (existingProfile != null)
                return new ErrorResult(_messages["UserProfileAlreadyExists"]);

            var profile = _mapper.Map<UserProfile>(dto);
            profile.UserId = userId;

            await _userProfileRepository.AddAsync(profile);
            await _userProfileRepository.SaveAsync();

            return new SuccessResult(_messages["UserProfileCreated"]);
        }

        public async Task<IResult> UpdateAsync(int userId, UserProfileDto dto)
        {
            var profile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: true);
            if (profile == null)
                return new ErrorResult(_messages["UserProfileNotFound"]);

            _mapper.Map(dto, profile);

            _userProfileRepository.Update(profile);
            await _userProfileRepository.SaveAsync();

            return new SuccessResult(_messages["UserProfileUpdated"]);
        }

        public async Task<IDataResult<UserProfileDto>> GetByUserIdAsync(int userId)
        {
            var profile = await _userProfileRepository.FirstOrDefaultAsync(p => p.UserId == userId, trackChanges: false);
            if (profile == null)
                return new ErrorDataResult<UserProfileDto>(_messages["UserProfileNotFound"]);

            var dto = _mapper.Map<UserProfileDto>(profile);

            return new SuccessDataResult<UserProfileDto>(dto, _messages["UserProfileFetched"]);
        }
    }
}
