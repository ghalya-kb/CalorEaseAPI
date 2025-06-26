namespace Entities.DTOs
{
    public class UserProfileDto
    {
        public int HeightCm { get; set; }
        public float WeightKg { get; set; }
        public int Age { get; set; }
        public string ActivityLevel { get; set; }
        public string GoalType { get; set; }
        public float BMR { get; set; }
        public float TDEE { get; set; }
        public float CalorieTarget { get; set; }
    }
}
