using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Descrição Obrigatório")]
    public string? Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Data Inicio Obrigatório")]
    public DateTime DateStart { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Data Final Obrigatório")]
    public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool MeetingType { get; set; } = false;
    public bool StatusSchedule { get; set; } = false;
    [Required(ErrorMessage = "Cliente Obrigatório")]
    public int ClientId { get; set; } = 0;
    public string? Client { get; set; } = string.Empty;
    [Required(ErrorMessage = "Usuário Obrigatório")]
    public int UserId { get; set; } = 0;
    public string? User { get; set; } = string.Empty;
}
