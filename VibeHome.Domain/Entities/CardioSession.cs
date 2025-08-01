namespace VibeHome.Domain.Entities
{
    public class CardioSession
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Distance { get; set; }
        public int CaloriesBurned { get; set; }
        public int CardioTypeId { get; set; }
        public int LocationId { get; set; }
    }
} 