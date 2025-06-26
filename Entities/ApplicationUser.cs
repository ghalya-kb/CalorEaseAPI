using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Entities
{
    public class ApplicationUser : IdentityUser<int>, IDbEntity
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserProfile Profile { get; set; }
    }
}
