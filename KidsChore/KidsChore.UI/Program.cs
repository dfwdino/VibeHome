using KidsChore.Application.Interfaces;
using KidsChore.Infrastructure.Data;
using KidsChore.Infrastructure.Repositories;
using KidsChore.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using KidsChore.UI;
using KidsChore.UI.Shared;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Add DbContext
builder.Services.AddDbContext<KidsChoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register application services
builder.Services.AddScoped<IKidService, KidService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IChoreItemService, ChoreItemService>();
builder.Services.AddScoped<IChoreCompletionService, ChoreCompletionService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IWeeklyPaymentStatusService, WeeklyPaymentStatusService>();
builder.Services.AddScoped<NotificationService>();

builder.Services.AddAuthorizationCore();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
