using Core.Entities;

namespace Entities
{
    public class Exercise : IDbEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Type { get; set; } // Walking, Running, etc.
        public float? CaloriesBurned { get; set; }
    }
}
