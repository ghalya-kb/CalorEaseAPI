using DataAccess.DbContext.EntityFrameworkCore;
using DataAccess.Repositories.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Concrete
{
    public class UserProfileRepository : RepositoryBase<UserProfile>, IUserProfileRepository
    {
        public UserProfileRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UserProfile?> GetByUserIdAsync(int userId)
        {
            return await _context.UserProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
