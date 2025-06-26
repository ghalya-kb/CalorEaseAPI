using Core.Entities;

namespace Entities
{
    public class WeightTracking : IDbEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public float WeightKg { get; set; }
    }
}
