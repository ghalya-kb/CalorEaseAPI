using Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContext.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<WaterIntake> WaterIntakes { get; set; }
        public DbSet<WeightTracking> WeightTrackings { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasIndex(x => x.Email).IsUnique();

            builder.Entity<ApplicationUser>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
