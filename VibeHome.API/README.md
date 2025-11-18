# VibeHome API

A RESTful API with OData support for managing the VibeHome application data. This API provides secure access to all entities in the VibeHome system using API key authentication.

## Features

- **OData Support**: Full OData v4 query capabilities including $filter, $select, $orderby, $expand, $top, and $count
- **API Key Authentication**: Secure access using SHA-256 hashed API keys stored in the database
- **Latest .NET 9.0**: Built with the latest .NET framework and C# features
- **Clean Architecture**: Follows the existing design patterns in the VibeHome solution
- **Swagger/OpenAPI**: Interactive API documentation at `/swagger`
- **Entity Framework Core**: Uses the existing DbContext and repositories

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- SQL Server (LocalDB or full instance)
- Existing VibeHome database

### Running the API

1. **Apply Database Migration** (First time only):
   ```bash
   cd VibeHome.Infrastructure
   dotnet ef migrations add AddApiKeyTable --startup-project ../VibeHome.API
   dotnet ef database update --startup-project ../VibeHome.API
   ```

2. **Run the API**:
   ```bash
   cd VibeHome.API
   dotnet run
   ```

3. **Access Swagger UI**:
   Navigate to `https://localhost:5001/swagger` (or the port shown in the console)

## Authentication

All API endpoints (except `/swagger` and `/health`) require an API key to be provided in the request header.

### Header Format

```
X-API-Key: your-api-key-here
```

### Generating an API Key

You can generate an API key using the API itself:

**Request:**
```http
POST /odata/ApiKeys/Generate
Content-Type: application/json

{
  "keyName": "My Application",
  "description": "API key for my mobile app",
  "expiresAt": "2025-12-31T23:59:59Z"  // Optional
}
```

**Response:**
```json
{
  "message": "API Key generated successfully. Please save this key as it will not be shown again.",
  "apiKey": "base64-encoded-random-key",
  "keyName": "My Application"
}
```

**IMPORTANT**: Save the API key immediately! It will not be shown again. The key is stored as a SHA-256 hash in the database for security.

### Managing API Keys

- **List all API keys**: `GET /odata/ApiKeys`
- **Get specific key**: `GET /odata/ApiKeys(1)`
- **Update key** (change name, description, active status): `PUT /odata/ApiKeys(1)`
- **Delete key** (soft delete): `DELETE /odata/ApiKeys(1)`

## Available Endpoints

All endpoints support standard OData queries. Base URL: `/odata/`

### Kids Domain
- `/odata/Kids` - Manage kids
- `/odata/ChoreItems` - Manage chore items
- `/odata/ChoreCompletions` - Manage chore completions
- `/odata/KidsChoreLocations` - Manage chore locations
- `/odata/Users` - Manage users
- `/odata/WeeklyPaymentStatuses` - Manage payment statuses

### Workout Domain
- `/odata/WeightTrainingSessions` - Manage weight training sessions
- `/odata/CardioSessions` - Manage cardio sessions
- `/odata/WorkoutTypes` - Manage workout types
- `/odata/CardioTypes` - Manage cardio types
- `/odata/WorkoutLocations` - Manage workout locations

### Weight Tracking
- `/odata/JournalEntries` - Manage journal entries

### Security
- `/odata/ApiKeys` - Manage API keys

## OData Query Examples

### Get all kids
```http
GET /odata/Kids
X-API-Key: your-api-key
```

### Get a specific kid by ID
```http
GET /odata/Kids(1)
X-API-Key: your-api-key
```

### Filter kids by name
```http
GET /odata/Kids?$filter=contains(Name, 'John')
X-API-Key: your-api-key
```

### Get top 10 chore completions, ordered by date
```http
GET /odata/ChoreCompletions?$top=10&$orderby=CompletedAt desc
X-API-Key: your-api-key
```

### Get chore completions with related kid and chore item data
```http
GET /odata/ChoreCompletions?$expand=Kid,ChoreItem
X-API-Key: your-api-key
```

### Select specific fields only
```http
GET /odata/Kids?$select=KidId,Name
X-API-Key: your-api-key
```

### Count records
```http
GET /odata/Kids/$count
X-API-Key: your-api-key
```

### Complex query example
```http
GET /odata/ChoreCompletions?$filter=CompletedAt ge 2025-01-01 and not IsDeleted&$orderby=CompletedAt desc&$expand=Kid,ChoreItem&$select=ChoreCompletionId,CompletedAt,AmountEarned&$top=20
X-API-Key: your-api-key
```

## CRUD Operations

### Create (POST)

```http
POST /odata/Kids
Content-Type: application/json
X-API-Key: your-api-key

{
  "name": "John Doe",
  "createdAt": "2025-11-18T00:00:00Z",
  "modifiedAt": "2025-11-18T00:00:00Z",
  "isDeleted": false
}
```

### Update (PUT)

```http
PUT /odata/Kids(1)
Content-Type: application/json
X-API-Key: your-api-key

{
  "kidId": 1,
  "name": "John Smith",
  "createdAt": "2025-11-18T00:00:00Z",
  "modifiedAt": "2025-11-18T12:00:00Z",
  "isDeleted": false
}
```

### Delete (DELETE)

```http
DELETE /odata/Kids(1)
X-API-Key: your-api-key
```

**Note**: Delete operations perform a soft delete by setting `IsDeleted = true`.

## Architecture

The API follows the existing Clean Architecture pattern:

- **VibeHome.API**: Web API layer with OData controllers and middleware
- **VibeHome.Application**: Service interfaces (IRepository<T>, IApiKeyService, etc.)
- **VibeHome.Infrastructure**: Implementation of services and repositories
- **VibeHome.Domain**: Entity models

## Security Features

1. **API Key Authentication**: All requests (except Swagger) require a valid API key
2. **SHA-256 Hashing**: API keys are hashed before storage
3. **Key Expiration**: Optional expiration dates for API keys
4. **Active/Inactive Status**: Keys can be deactivated without deletion
5. **Soft Delete**: Deleted keys are marked as deleted, not removed
6. **Last Used Tracking**: Each key tracks its last usage timestamp

## CORS Configuration

The API is configured to accept requests from any origin. For production, update the CORS policy in `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

## Troubleshooting

### "API Key is missing" Error
- Ensure the `X-API-Key` header is included in your request
- Check that the header name is exactly `X-API-Key` (case-sensitive)

### "API Key is invalid" Error
- Verify the API key is correct
- Check if the key has expired (check `ExpiresAt` field)
- Ensure the key is active (`IsActive = true`)
- Verify the key hasn't been deleted (`IsDeleted = false`)

### Database Connection Issues
- Verify the connection string in `appsettings.json`
- Ensure SQL Server is running
- Run database migrations if not already applied

## Further Reading

- [OData v4 Query Options](https://docs.oasis-open.org/odata/odata/v4.0/odata-v4.0-part2-url-conventions.html)
- [ASP.NET Core OData 9.x](https://learn.microsoft.com/en-us/odata/webapi-9/overview)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
