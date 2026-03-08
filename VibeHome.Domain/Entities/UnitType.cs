namespace VibeHome.Domain.Entities
{
    public class UnitType
    {
        public int UnitTypeId { get; set; }
        public string UnitName { get; set; } = string.Empty;
        public string? Abbreviation { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
