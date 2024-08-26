using System.ComponentModel.DataAnnotations;

namespace TodoApi.GeographyAPI.Models;

public sealed class Provincia
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Codigo { get; set; }
    [Required]
    public string Nombre { get; set; }
}