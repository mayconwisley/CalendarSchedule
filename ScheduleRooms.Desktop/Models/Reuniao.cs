using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleRooms.Models;

public class Reuniao
{
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(1000)")]
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime DateStart { get; set; }
    [Required]
    public DateTime DataFim { get; set; }
    public bool AllowCall { get; set; }
    public bool AllowChat { get; set; }
    public int RoomId { get; set; }
    public virtual Room Room { get; set; }
}
