var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Urls.Add("http://0.0.0.0:8080");

app.MapGet("/", () => "Hello World!");

app.Run();
