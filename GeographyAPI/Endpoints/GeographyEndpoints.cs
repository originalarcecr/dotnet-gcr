using TodoApi.GeographyAPI.Services;

namespace TodoApi.GeographyAPI.Endpoints;

public static class GeographyEndpoints
{
    public static void MapGeographyEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/provincias", async (IGeographyService service) =>
            await service.GetProvinciasAsync());

        app.MapGet("/cantones/{provinciaId}", async (string provinciaId, IGeographyService service) =>
            await service.GetCantonesAsync(provinciaId));

        app.MapGet("/distritos/{cantonId}", async (string cantonId, IGeographyService service) =>
            await service.GetDistritosAsync(cantonId));

        app.MapGet("/barrios/{distritoId}", async (string distritoId, IGeographyService service) =>
            await service.GetBarriosAsync(distritoId));

        app.MapPost("/import/provincias", async (HttpRequest request, IGeographyService service) =>
        {
            try
            {
                var file = request.Form.Files.GetFile("file");
                if (file == null)
                    return Results.BadRequest("No file uploaded");

                using var stream = file.OpenReadStream();
                await service.ImportProvinciasFromJsonAsync(stream);
                return Results.Ok("Provincias imported successfully");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Error importing provincias: {ex.Message}", statusCode: 500);
            }
        });
    }
}