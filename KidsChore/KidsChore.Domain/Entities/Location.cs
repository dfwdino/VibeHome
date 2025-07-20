namespace KidsChore.Domain.Entities
{
    public class Location
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
} 