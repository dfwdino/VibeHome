namespace VibeHome.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // 'Parent', 'Partner', or 'Child'
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
} 