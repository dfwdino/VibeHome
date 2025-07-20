using GymTracker.Application.Services;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using GymTracker.Infrastructure.Repositories;
using KidsChore.Application.Interfaces;
using KidsChore.Infrastructure.Data;
using KidsChore.Infrastructure.Repositories;
using KidsChore.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using VibeHome.UI.Data;
using GymTrackerLocationService = GymTracker.Application.Services.LocationService;
using KidsChoreLocationService = KidsChore.Infrastructure.Services.LocationService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(options =>
{
    options.DetailedErrors = true;
    options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
    options.MaxBufferedUnacknowledgedRenderBatches = 10;
});

// Configure SignalR with better transport options
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 32 * 1024; // 32KB
});
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<VibeHome.UI.Shared.NotificationService>();
builder.Services.AddScoped<AuthenticationStateProvider, VibeHome.UI.CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IChoreItemService, ChoreItemService>();
builder.Services.AddScoped<IChoreCompletionService, ChoreCompletionService>();
builder.Services.AddScoped<IKidService, KidService>();
builder.Services.AddScoped<ILocationService, KidsChoreLocationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWeeklyPaymentStatusService, WeeklyPaymentStatusService>();
builder.Services.AddDbContext<KidsChoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KidsChoreConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("KidsChoreConnection")));
builder.Services.AddScoped<WeightTrainingSessionService>();
builder.Services.AddScoped<CardioSessionService>();
builder.Services.AddScoped<GymTrackerLocationService>();
builder.Services.AddScoped<WorkoutTypeService>();
builder.Services.AddScoped<CardioTypeService>();
builder.Services.AddScoped<IWeightTrainingSessionRepository, WeightTrainingSessionRepository>();
builder.Services.AddScoped<ICardioSessionRepository, CardioSessionRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
builder.Services.AddScoped<ICardioTypeRepository, CardioTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
