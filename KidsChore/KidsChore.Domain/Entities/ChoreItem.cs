namespace KidsChore.Domain.Entities
{
    public class ChoreItem
    {
        public int ChoreItemId { get; set; }
        public string ChoreName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
} 