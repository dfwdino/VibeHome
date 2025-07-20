using System;

namespace KidsChore.Domain.Entities
{
    public class WeeklyPaymentStatus
    {
        public int WeeklyPaymentStatusId { get; set; }
        public int KidId { get; set; }
        public DateTime WeekStartDate { get; set; }
        public bool IsPaid { get; set; }

        public Kid? Kid { get; set; } // Navigation property
    }
} 