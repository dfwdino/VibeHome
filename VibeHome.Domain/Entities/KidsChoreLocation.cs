using System.ComponentModel.DataAnnotations;

namespace VibeHome.Domain.Entities
{
    public class KidsChoreLocation
    {
        [Key]
        public int LocationId { get; set; }
        public string LocationName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
} 