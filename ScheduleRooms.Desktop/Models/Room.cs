using System.ComponentModel.DataAnnotations.Schema;

namespace CalendarSchedule.Models;

public class Room
{
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(500)")]
    public string Name { get; set; }
    [Column(TypeName = "VARCHAR(5)")]
    public string Ramal { get; set; }
    [Column(TypeName = "VARCHAR(1000)")]
    public string Description { get; set; }
}
