using Entities;

namespace DataAccess.Repositories.Abstract
{
    public interface IUserProfileRepository : IRepositoryBase<UserProfile>
    {
        Task<UserProfile?> GetByUserIdAsync(int userId);
    }
}
