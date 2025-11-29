using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using VibeHome.Infrastructure.Data;
using VibeHome.Infrastructure.Repositories;
using VibeHome.Infrastructure.Services;
using VibeHome.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Configure OData
var modelBuilder = new ODataConventionModelBuilder();

// Register all entity sets for OData
modelBuilder.EntitySet<Kid>("Kids");
modelBuilder.EntitySet<KidsChoreLocation>("KidsChoreLocations");
modelBuilder.EntitySet<ChoreItem>("ChoreItems");
modelBuilder.EntitySet<ChoreCompletion>("ChoreCompletions");
modelBuilder.EntitySet<User>("Users");
modelBuilder.EntitySet<WeeklyPaymentStatus>("WeeklyPaymentStatuses");
modelBuilder.EntitySet<JournalEntry>("JournalEntries");
modelBuilder.EntitySet<ApiKey>("ApiKeys");
modelBuilder.EntitySet<WeightTrainingSession>("WeightTrainingSessions");
modelBuilder.EntitySet<CardioSession>("CardioSessions");
modelBuilder.EntitySet<WorkoutType>("WorkoutTypes");
modelBuilder.EntitySet<CardioType>("CardioTypes");
modelBuilder.EntitySet<WorkoutLocation>("WorkoutLocations");

builder.Services.AddControllers()
    .AddOData(options => options
        .Select()
        .Filter()
        .OrderBy()
        .Expand()
        .Count()
        .SetMaxTop(100)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));

// Configure DbContext
builder.Services.AddDbContext<VibeHomeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register services
builder.Services.AddScoped<IKidService, KidService>();
builder.Services.AddScoped<IChoreItemService, ChoreItemService>();
builder.Services.AddScoped<IChoreCompletionService, ChoreCompletionService>();
builder.Services.AddScoped<IKidsChoreLocationService, KidsChoreLocationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWeeklyPaymentStatusService, WeeklyPaymentStatusService>();
builder.Services.AddScoped<IJournalEntryService, JournalEntryService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "VibeHome API", Version = "v1" });

    // Add API Key authentication to Swagger
    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key authentication. Enter your API key in the text input below.",
        Name = "X-API-Key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors();

// Use API Key Authentication Middleware
app.UseApiKeyAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
