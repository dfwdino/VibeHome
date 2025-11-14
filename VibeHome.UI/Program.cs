using VibeHome.Application.Interfaces;
using VibeHome.Infrastructure.Data;
using VibeHome.Infrastructure.Repositories;
using VibeHome.Infrastructure.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using VibeHome.UI.Data;

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
// KidsChore Services
builder.Services.AddScoped<IChoreItemService, ChoreItemService>();
builder.Services.AddScoped<IChoreCompletionService, ChoreCompletionService>();
builder.Services.AddScoped<IKidService, KidService>();
builder.Services.AddScoped<IKidsChoreLocationService, KidsChoreLocationService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWeeklyPaymentStatusService, WeeklyPaymentStatusService>();

// Workout Services
builder.Services.AddScoped<ICardioSessionService, CardioSessionService>();
builder.Services.AddScoped<ICardioTypeService, CardioTypeService>();
builder.Services.AddScoped<IWeightTrainingSessionService, WeightTrainingSessionService>();
builder.Services.AddScoped<IWorkoutTypeService, WorkoutTypeService>();
builder.Services.AddScoped<IWorkoutLocationService, WorkoutLocationService>();
builder.Services.AddScoped<IJournalEntryService, VibeHome.Infrastructure.Services.JournalEntryService>();

// Database and Repository
builder.Services.AddDbContext<VibeHomeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VibeHomeConnection")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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
