using Microsoft.EntityFrameworkCore;

using TodoApi.AttendanceAPI.Endpoints;
using TodoApi.AttendanceAPI.Services;

using TodoApi.GeographyAPI.Data;
using TodoApi.GeographyAPI.Endpoints;
using TodoApi.GeographyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGeographyService, GeographyService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();

// Configure SQLite
var databasePath = Environment.GetEnvironmentVariable("DatabasePath") ?? "todo.db";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:8080");

app.MapGet("/", () => "Hello World!");

app.MapGeographyEndpoints();
app.MapAttendanceEndpoints();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
