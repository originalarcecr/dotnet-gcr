using Microsoft.EntityFrameworkCore;
using TodoApi.GeographyAPI.Models;

namespace TodoApi.GeographyAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Provincia> Provincias { get; set; }
    public DbSet<Canton> Cantones { get; set; }
    public DbSet<Distrito> Distritos { get; set; }
    public DbSet<Barrio> Barrios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Codigo).IsUnique();
        });

        modelBuilder.Entity<Canton>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasOne(c => c.Provincia)
                  .WithMany()
                  .HasForeignKey(c => c.ProvinciaId);
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasOne(d => d.Canton)
                  .WithMany()
                  .HasForeignKey(d => d.CantonId);
        });

        modelBuilder.Entity<Barrio>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Codigo).IsUnique();
            entity.HasOne(b => b.Distrito)
                  .WithMany()
                  .HasForeignKey(b => b.DistritoId);
        });
    }
}