namespace Entities.DTOs
{
    public class MealDto
    {
        public string MealType { get; set; } // Breakfast, Lunch, Dinner
        public DateTime Date { get; set; }
        public List<MealItemDto> MealItems { get; set; } = new();
    }
}
