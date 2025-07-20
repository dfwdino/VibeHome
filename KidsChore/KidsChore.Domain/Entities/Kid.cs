namespace KidsChore.Domain.Entities
{
    public class Kid
    {
        public int KidId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; }
        public string? Grade { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
} 