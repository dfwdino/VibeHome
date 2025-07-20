namespace KidsChore.Domain.Entities
{
    public class ChoreCompletion
    {
        public int ChoreCompletionId { get; set; }
        public int KidId { get; set; }
        public int ChoreItemId { get; set; }
        public int LocationId { get; set; }
        public DateTime CompletionDateTime { get; set; }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public decimal Price { get; set; }

        public Kid? Kid { get; set; }
        public ChoreItem? ChoreItem { get; set; }
        public Location? Location { get; set; }
    }
} 