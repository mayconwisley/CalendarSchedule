using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Descrição Obrigatório")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "Data Inicio Obrigatório")]
    public DateTime DateStart { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Data Final Obrigatório")]
    public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool AllowCall { get; set; } = false;
    public bool AllowChat { get; set; } = false;
    [Required(ErrorMessage = "Room Obrigatória")]
    public int RoomId { get; set; }
    public string? Room { get; set; }
}
