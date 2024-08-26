using Microsoft.EntityFrameworkCore;

using TodoApi.GeographyAPI.Data;
using TodoApi.GeographyAPI.Endpoints;
using TodoApi.GeographyAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGeographyService, GeographyService>();

var app = builder.Build();

// app.Urls.Add("http://0.0.0.0:8080");

app.MapGet("/", () => "Hello World!");

app.MapGeographyEndpoints();

app.Run();
