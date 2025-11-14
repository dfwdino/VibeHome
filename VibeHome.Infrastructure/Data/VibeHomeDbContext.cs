using System;
using VibeHome.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace VibeHome.Infrastructure.Data
{
    public class VibeHomeDbContext : DbContext
    {
        public VibeHomeDbContext(DbContextOptions<VibeHomeDbContext> options) : base(options) { }

        // KidsChore Domain
        public DbSet<Kid> Kids { get; set; }
        public DbSet<KidsChoreLocation> KidsChoreLocations { get; set; }
        public DbSet<ChoreItem> ChoreItems { get; set; }
        public DbSet<ChoreCompletion> ChoreCompletions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeeklyPaymentStatus> WeeklyPaymentStatuses { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }

        // Workout Domain
        public DbSet<WeightTrainingSession> WeightTrainingSessions { get; set; }
        public DbSet<CardioSession> CardioSessions { get; set; }
        public DbSet<WorkoutType> WorkoutTypes { get; set; }
        public DbSet<CardioType> CardioTypes { get; set; }
        public DbSet<WorkoutLocation> WorkoutLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // KidsChore Domain Schema
            modelBuilder.Entity<Kid>().ToTable("Kids", "Kids");
            modelBuilder.Entity<Kid>().HasKey(k => k.KidId);
            
            modelBuilder.Entity<KidsChoreLocation>().ToTable("Locations", "Kids");
            modelBuilder.Entity<KidsChoreLocation>().HasKey(l => l.LocationId);
            
            modelBuilder.Entity<ChoreItem>().ToTable("ChoreItems", "Kids");
            modelBuilder.Entity<ChoreItem>().HasKey(c => c.ChoreItemId);
            
            modelBuilder.Entity<ChoreCompletion>().ToTable("ChoreCompletions", "Kids");
            modelBuilder.Entity<ChoreCompletion>().HasKey(c => c.ChoreCompletionId);
            
            modelBuilder.Entity<User>().ToTable("Users", "Kids");
            modelBuilder.Entity<User>().HasKey(u => u.UserId);
            
            modelBuilder.Entity<WeeklyPaymentStatus>().ToTable("WeeklyPaymentStatuses", "Kids");
            modelBuilder.Entity<WeeklyPaymentStatus>().HasKey(w => w.WeeklyPaymentStatusId);

            // Workout Domain Schema
            modelBuilder.Entity<CardioSession>().ToTable("CardioSessions", "Workout");
            modelBuilder.Entity<CardioSession>().HasKey(c => c.Id);
            
            modelBuilder.Entity<CardioType>().ToTable("CardioTypes", "Workout");
            modelBuilder.Entity<CardioType>().HasKey(c => c.Id);
            
            modelBuilder.Entity<WorkoutType>().ToTable("WorkoutTypes", "Workout");
            modelBuilder.Entity<WorkoutType>().HasKey(w => w.Id);
            
            modelBuilder.Entity<WorkoutLocation>().ToTable("Locations", "Workout");
            modelBuilder.Entity<WorkoutLocation>().HasKey(w => w.Id);
            
            modelBuilder.Entity<WeightTrainingSession>().ToTable("WeightTrainingSessions", "Workout");
            modelBuilder.Entity<WeightTrainingSession>().HasKey(w => w.Id);

            modelBuilder.Entity<JournalEntry>().ToTable("Journal", "Weight");
            modelBuilder.Entity<JournalEntry>().HasKey(j => j.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
} 