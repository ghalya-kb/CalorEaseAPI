using Core.Entities;

namespace Entities
{
    public class WaterIntake : IDbEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime Date { get; set; }
        public int Cups { get; set; }
    }
}
