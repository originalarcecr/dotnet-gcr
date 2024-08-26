namespace TodoApi.GeographyAPI.Models;
public class Distrito 
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public int? CantonId { get; set; }
    public virtual Canton Canton { get; set; }
}