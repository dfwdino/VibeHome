using System.ComponentModel.DataAnnotations;

namespace VibeHome.Domain.Entities
{
    public class ChoreCompletion
    {
      
        public int ChoreCompletionId { get; set; }
        [Required(ErrorMessage = "Please select a kid")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a kid")]
        public int KidId { get; set; }
        public int ChoreItemId { get; set; }

        [Required(ErrorMessage = "Please select a Location")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Location")]
        public int LocationId { get; set; }
        public DateTime CompletionDateTime
        {
            get => field;
            set => field = DateTime.SpecifyKind(value, DateTimeKind.Local);
        }
        public string? Notes { get; set; }
        public bool IsDeleted { get; set; }
    
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public decimal Price { get; set; }

        public Kid? Kid { get; set; }
        public ChoreItem? ChoreItem { get; set; }
        public KidsChoreLocation? Location { get; set; }
    }


} 