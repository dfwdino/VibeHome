using System.ComponentModel.DataAnnotations;

namespace VibeHome.Domain.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal Pounds { get; set; }

        [Required]
        [Range(0, 1000)]
        public decimal WaistSize { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }
    }
}
