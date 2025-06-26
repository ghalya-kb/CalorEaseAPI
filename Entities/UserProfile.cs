using Core.Entities;

namespace Entities
{
    public class UserProfile : IDbEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int HeightCm { get; set; }
        public float WeightKg { get; set; }
        public int Age { get; set; }

        public string ActivityLevel { get; set; } // Low, Medium, High
        public string GoalType { get; set; }      // Lose, Maintain, Gain

        public float BMR { get; set; }
        public float TDEE { get; set; }
        public float CalorieTarget { get; set; }
    }
}
