namespace Entities.DTOs
{
    public class MealItemDto
    {
        public string FoodName { get; set; }
        public float AmountGr { get; set; }
        public float Calories { get; set; }
        public float? Protein { get; set; }
        public float? Carbs { get; set; }
        public float? Fat { get; set; }
    }
}
