namespace TodoApi.GeographyAPI.Models;
public class Barrio 
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int? DistritoId { get; set; }
    public virtual Distrito Distrito { get; set; }
}