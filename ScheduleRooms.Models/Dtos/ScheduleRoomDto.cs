using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleRoomDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Descrição Obrigatório")]
    public string? Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Data Inicio Obrigatório")]
    public DateTime DateStart { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Data Final Obrigatório")]
    public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool AllowCall { get; set; } = false;
    public bool AllowChat { get; set; } = false;
    [Required(ErrorMessage = "Sala Obrigatória")]
    public int RoomId { get; set; } = 0;
    public string? Room { get; set; } = string.Empty;
}
