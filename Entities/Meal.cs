using Core.Entities;

namespace Entities
{
    public class Meal : IDbEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string MealType { get; set; } // Breakfast, Lunch, Dinner
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<MealItem> MealItems { get; set; }
    }
}
