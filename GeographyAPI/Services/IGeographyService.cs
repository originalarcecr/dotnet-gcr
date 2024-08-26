using TodoApi.GeographyAPI.Models;

namespace TodoApi.GeographyAPI.Services;

public interface IGeographyService
{
    Task<IEnumerable<Provincia>> GetProvinciasAsync();
    Task<IEnumerable<Canton>> GetCantonesAsync(string provinciaId);
    Task<IEnumerable<Distrito>> GetDistritosAsync(string cantonId);
    Task<IEnumerable<Barrio>> GetBarriosAsync(string distritoId);

    Task ImportProvinciasFromJsonAsync(Stream fileStream);
}