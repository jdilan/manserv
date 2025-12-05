/*
================================================================================
MANSERV Loan Account Management System - Local Testing API
Main Program Entry Point
================================================================================
Purpose: Minimal API for local testing of Unit 1 endpoints
Technology: ASP.NET Core 6.0 Minimal API
Author: System Generated
Date: December 6, 2025

USAGE:
  dotnet run --project ManservLocalTestAPI.csproj
  
  API will be available at: http://localhost:5000
  Swagger UI: http://localhost:5000/swagger
================================================================================
*/

using System.Data.SQLite;
using ManservLoanSystem.LocalTesting.API.Middleware;
using ManservLoanSystem.LocalTesting.Mocks;
using ManservLoanSystem.LocalTesting.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// ============================================================================
// CONFIGURATION
// ============================================================================

// Configure Kestrel to listen on port 5000
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5000);
});

// ============================================================================
// SERVICES CONFIGURATION
// ============================================================================

// Add controllers
builder.Services.AddControllers();

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS for local browser testing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ============================================================================
// DEPENDENCY INJECTION - REPOSITORIES
// ============================================================================

// SQLite connection string
var connectionString = "Data Source=manserv_test.db;Version=3;";

// Register SQLite repository
builder.Services.AddScoped<SqliteAccountRepository>(provider =>
{
    return new SqliteAccountRepository(connectionString);
});

// ============================================================================
// DEPENDENCY INJECTION - MOCK SERVICES
// ============================================================================

// Register mock services as singletons (stateless)
builder.Services.AddSingleton<ReferenceDataServiceMock>();
builder.Services.AddSingleton<ValidationServiceMock>(provider =>
{
    var config = MockConfigManager.Instance.Config;
    return new ValidationServiceMock(config.Validation.StrictMode);
});
builder.Services.AddSingleton<AccessControlServiceMock>();
builder.Services.AddSingleton<CustomerServiceMock>();

// ============================================================================
// BUILD APPLICATION
// ============================================================================

var app = builder.Build();

// ============================================================================
// MIDDLEWARE PIPELINE
// ============================================================================

// Enable Swagger in all environments (this is a test API)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MANSERV Local Test API v1");
    c.RoutePrefix = "swagger";
});

// Enable CORS
app.UseCors("AllowLocalhost");

// Add request logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();

// Enable routing
app.UseRouting();

// Map controllers
app.MapControllers();

// ============================================================================
// DATABASE INITIALIZATION
// ============================================================================

// Initialize SQLite database if it doesn't exist
InitializeDatabase(connectionString);

// ============================================================================
// ROOT ENDPOINT
// ============================================================================

app.MapGet("/", () => new
{
    message = "MANSERV Local Test API - Unit 1: Loan Account Management",
    version = "1.0",
    endpoints = new
    {
        swagger = "/swagger",
        accounts = "/api/accounts",
        search = "/api/accounts/search",
        referenceData = "/api/reference",
        health = "/api/health"
    },
    documentation = "See /swagger for full API documentation"
});

// ============================================================================
// HEALTH CHECK ENDPOINT
// ============================================================================

app.MapGet("/api/health", (SqliteAccountRepository repo) =>
{
    try
    {
        // Test database connection
        var accounts = repo.GetAll();
        
        return Results.Ok(new
        {
            status = "healthy",
            database = "connected",
            accountCount = accounts.Count,
            timestamp = DateTime.Now
        });
    }
    catch (Exception ex)
    {
        return Results.Ok(new
        {
            status = "unhealthy",
            database = "disconnected",
            error = ex.Message,
            timestamp = DateTime.Now
        });
    }
});

// ============================================================================
// RUN APPLICATION
// ============================================================================

Console.WriteLine("================================================================================");
Console.WriteLine("MANSERV Local Test API - Unit 1: Loan Account Management");
Console.WriteLine("================================================================================");
Console.WriteLine($"API Server: http://localhost:5000");
Console.WriteLine($"Swagger UI: http://localhost:5000/swagger");
Console.WriteLine($"Database: {connectionString}");
Console.WriteLine("================================================================================");
Console.WriteLine("Press Ctrl+C to stop the server");
Console.WriteLine("================================================================================");

app.Run();

// ============================================================================
// HELPER METHODS
// ============================================================================

void InitializeDatabase(string connString)
{
    try
    {
        // Check if database file exists
        var dbFile = connString.Split(';')[0].Replace("Data Source=", "").Trim();
        
        if (!File.Exists(dbFile))
        {
            Console.WriteLine($"Creating SQLite database: {dbFile}");
            
            // Create database file
            SQLiteConnection.CreateFile(dbFile);
            
            // Run schema script
            var schemaScript = File.ReadAllText("../Database/SQLite_Schema.sql");
            using var connection = new SQLiteConnection(connString);
            connection.Open();
            
            using var command = new SQLiteCommand(schemaScript, connection);
            command.ExecuteNonQuery();
            
            Console.WriteLine("Database schema created successfully");
            
            // Run sample data script
            var dataScript = File.ReadAllText("../Database/SQLite_SampleData.sql");
            using var dataCommand = new SQLiteCommand(dataScript, connection);
            dataCommand.ExecuteNonQuery();
            
            Console.WriteLine("Sample data inserted successfully");
        }
        else
        {
            Console.WriteLine($"Using existing database: {dbFile}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Warning: Database initialization failed: {ex.Message}");
        Console.WriteLine("You may need to manually create the database using the SQL scripts");
    }
}
