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

builder.Services.AddHttpClient("VibeHomeApi", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
    client.DefaultRequestHeaders.Add("X-API-Key", apiKey);
    client.Timeout = TimeSpan.FromSeconds(30);
});

// KidsChore HTTP Services (calling API)
builder.Services.AddScoped<IChoreItemService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new ChoreItemHttpService(httpClient);
});

builder.Services.AddScoped<IChoreCompletionService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new ChoreCompletionHttpService(httpClient);
});

builder.Services.AddScoped<IKidService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new KidHttpService(httpClient);
});

builder.Services.AddScoped<IKidsChoreLocationService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new KidsChoreLocationHttpService(httpClient);
});

builder.Services.AddScoped<IUserService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new UserHttpService(httpClient);
});

builder.Services.AddScoped<IWeeklyPaymentStatusService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new WeeklyPaymentStatusHttpService(httpClient);
});

// Workout HTTP Services (calling API)
builder.Services.AddScoped<ICardioSessionService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new CardioSessionHttpService(httpClient);
});

builder.Services.AddScoped<ICardioTypeService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new CardioTypeHttpService(httpClient);
});

builder.Services.AddScoped<IWeightTrainingSessionService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new WeightTrainingSessionHttpService(httpClient);
});

builder.Services.AddScoped<IWorkoutTypeService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new WorkoutTypeHttpService(httpClient);
});

builder.Services.AddScoped<IWorkoutLocationService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new WorkoutLocationHttpService(httpClient);
});

builder.Services.AddScoped<IJournalEntryService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new JournalEntryHttpService(httpClient);
});

// ReportService HTTP Service (calling API)
builder.Services.AddScoped<IReportService>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("VibeHomeApi");
    return new ReportHttpService(httpClient);
});

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
