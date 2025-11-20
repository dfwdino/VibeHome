using System.ComponentModel.DataAnnotations;

namespace VibeHome.Domain.Entities
{
    public class ApiKey
    {
        public int ApiKeyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string KeyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string KeyValue { get; set; } = string.Empty; // Hashed API key

        [MaxLength(1000)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime? ExpiresAt { get; set; }

        public DateTime? LastUsedAt { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    }
}
