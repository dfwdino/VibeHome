using System;
using KidsChore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KidsChore.Infrastructure.Data
{
    public class KidsChoreDbContext : DbContext
    {
        public KidsChoreDbContext(DbContextOptions<KidsChoreDbContext> options) : base(options) { }

        public DbSet<Kid> Kids { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ChoreItem> ChoreItems { get; set; }
        public DbSet<ChoreCompletion> ChoreCompletions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeeklyPaymentStatus> WeeklyPaymentStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Kids");
            base.OnModelCreating(modelBuilder);
        }
    }
} 