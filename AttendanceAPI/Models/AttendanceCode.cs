using System.ComponentModel.DataAnnotations;

namespace TodoApi.AttendanceAPI.Models;
public sealed class AttendanceCode
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }
}