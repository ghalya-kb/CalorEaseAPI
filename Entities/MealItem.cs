using Core.Entities;

namespace Entities
{
    public class MealItem : IDbEntity
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public string FoodName { get; set; }
        public float AmountGr { get; set; }
        public float Calories { get; set; }
        public float? Protein { get; set; }
        public float? Carbs { get; set; }
        public float? Fat { get; set; }
    }
}
