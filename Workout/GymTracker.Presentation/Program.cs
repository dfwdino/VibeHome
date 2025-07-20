using GymTracker.Application.Services;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using GymTracker.Infrastructure.Repositories;
using GymTracker.Presentation.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IWeightTrainingSessionRepository, WeightTrainingSessionRepository>();
builder.Services.AddScoped<ICardioSessionRepository, CardioSessionRepository>();
builder.Services.AddScoped<IWorkoutTypeRepository, WorkoutTypeRepository>();
builder.Services.AddScoped<ICardioTypeRepository, CardioTypeRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();

// Register services
builder.Services.AddScoped<WeightTrainingSessionService>();
builder.Services.AddScoped<CardioSessionService>();
builder.Services.AddScoped<WorkoutTypeService>();
builder.Services.AddScoped<CardioTypeService>();
builder.Services.AddScoped<LocationService>();

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
