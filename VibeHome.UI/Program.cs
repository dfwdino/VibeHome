using VibeHome.Application.Interfaces;
using VibeHome.Infrastructure.HttpServices;
using Microsoft.AspNetCore.Components.Authorization;
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

// Configure HttpClient for API calls
var apiBaseUrl = builder.Configuration["VibeHomeApi:BaseUrl"] ?? "";
var apiKey = builder.Configuration["VibeHomeApi:ApiKey"] ?? "TestAPI";

// Helper action to configure all API HttpClients with the same settings
Action<HttpClient> configureApiClient = client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
    client.Timeout = TimeSpan.FromSeconds(30);
};

// Register typed HttpClients for all services
builder.Services.AddHttpClient<IChoreItemService, ChoreItemHttpService>(configureApiClient);
builder.Services.AddHttpClient<IChoreCompletionService, ChoreCompletionHttpService>(configureApiClient);
builder.Services.AddHttpClient<IKidService, KidHttpService>(configureApiClient);
builder.Services.AddHttpClient<IKidsChoreLocationService, KidsChoreLocationHttpService>(configureApiClient);
builder.Services.AddHttpClient<IUserService, UserHttpService>(configureApiClient);
builder.Services.AddHttpClient<IWeeklyPaymentStatusService, WeeklyPaymentStatusHttpService>(configureApiClient);
builder.Services.AddHttpClient<ICardioSessionService, CardioSessionHttpService>(configureApiClient);
builder.Services.AddHttpClient<ICardioTypeService, CardioTypeHttpService>(configureApiClient);
builder.Services.AddHttpClient<IWeightTrainingSessionService, WeightTrainingSessionHttpService>(configureApiClient);
builder.Services.AddHttpClient<IWorkoutTypeService, WorkoutTypeHttpService>(configureApiClient);
builder.Services.AddHttpClient<IWorkoutLocationService, WorkoutLocationHttpService>(configureApiClient);
builder.Services.AddHttpClient<IJournalEntryService, JournalEntryHttpService>(configureApiClient);
builder.Services.AddHttpClient<IReportService, ReportHttpService>(configureApiClient);

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
