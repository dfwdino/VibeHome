using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VibeHome.Domain.Entities
{
    public class WeeklyPaymentStatus
    {
        [Key]
        public int WeeklyPaymentStatusId { get; set; }
        public int KidId { get; set; }

        [JsonPropertyName("weekStartDate")]
        public DateTime WeekStartDate { get; set; }
        public bool IsPaid { get; set; }

        public Kid? Kid { get; set; } // Navigation property

    }
} 