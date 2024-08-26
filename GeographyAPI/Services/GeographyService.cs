using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TodoApi.GeographyAPI.Data;
using TodoApi.GeographyAPI.Models;

namespace TodoApi.GeographyAPI.Services;

public class GeographyService : IGeographyService
{
    private readonly ApplicationDbContext _context;

    public GeographyService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Provincia>> GetProvinciasAsync()
    {
        return await _context.Provincias.ToListAsync();
    }

    public async Task<IEnumerable<Canton>> GetCantonesAsync(string provinciaId)
    {
        return await _context.Cantones
            .Where(c => c.ProvinciaId == int.Parse(provinciaId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Distrito>> GetDistritosAsync(string cantonId)
    {
        return await _context.Distritos
            .Where(d => d.CantonId == int.Parse(cantonId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Barrio>> GetBarriosAsync(string distritoId)
    {
        return await _context.Barrios
            .Where(b => b.DistritoId == int.Parse(distritoId))
            .ToListAsync();
    }

    public async Task ImportProvinciasFromJsonAsync(Stream fileStream)
    {
        using var reader = new StreamReader(fileStream);
        var jsonString = await reader.ReadToEndAsync();
        var provincias = JsonSerializer.Deserialize<List<Provincia>>(jsonString);
        
        if (provincias != null)
        {
            foreach (var provincia in provincias)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                
                try
                {
                    var existingProvincia = await _context.Provincias
                        .FirstOrDefaultAsync(p => p.Codigo == provincia.Codigo);

                    if (existingProvincia == null)
                    {
                        await _context.Provincias.AddAsync(provincia);
                    }
                    else
                    {
                        existingProvincia.Nombre = provincia.Nombre;
                        _context.Provincias.Update(existingProvincia);
                    }
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                }
            }
        }
    }
}