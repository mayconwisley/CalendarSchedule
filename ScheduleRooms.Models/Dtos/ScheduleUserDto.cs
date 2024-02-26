using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserDto
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
    [Required(ErrorMessage = "Cliente Obrigatório")]
    public int ClientId { get; set; }
    public string? Client { get; set; }
    [Required(ErrorMessage = "Usuário Obrigatório")]
    public int UserId { get; set; }
    public string? User { get; set; }
}
