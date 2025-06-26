using Core.Utilities.Result;
using Entities.DTOs;
using Entities;

namespace Business.Abstract
{
    public interface IUserProfileService
    {
        Task<IResult> CreateAsync(int userId, UserProfileDto dto);
        Task<IResult> UpdateAsync(int userId, UserProfileDto dto);
        Task<IDataResult<UserProfile>> GetByUserIdAsync(int userId);
    }
}
