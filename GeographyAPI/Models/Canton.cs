namespace TodoApi.GeographyAPI.Models;

public class Canton 
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int? ProvinciaId { get; set; }
    public virtual Provincia Provincia { get; set; }
}