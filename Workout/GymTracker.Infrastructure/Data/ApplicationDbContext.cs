using Microsoft.EntityFrameworkCore;
using GymTracker.Domain.Models;

namespace GymTracker.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WeightTrainingSession> WeightTrainingSessions { get; set; }
    public DbSet<CardioSession> CardioSessions { get; set; }
    public DbSet<WorkoutType> WorkoutTypes { get; set; }
    public DbSet<CardioType> CardioTypes { get; set; }
    public DbSet<Location> Locations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CardioSession>().ToTable("CardioSessions", "workout");
        modelBuilder.Entity<CardioType>().ToTable("CardioTypes", "workout");
        modelBuilder.Entity<WorkoutType>().ToTable("WorkoutTypes", "workout");
        modelBuilder.Entity<Location>().ToTable("Locations", "workout");
        modelBuilder.Entity<WeightTrainingSession>().ToTable("WeightTrainingSessions", "workout");
    }
} 