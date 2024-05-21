using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserDto
{
    public int Id { get; set; } = 0;
    public string? Description { get; set; } = null;
    [Required(ErrorMessage = "Data Inicio Obrigatório")]
    public DateTime DateStart { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Data Final Obrigatório")]
    public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool MeetingType { get; set; } = false;
    public bool StatusSchedule { get; set; } = false;
    public bool Particular { get; set; } = false;
    public int? ClientId { get; set; } = null;
    public string? Client { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    [Required(ErrorMessage = "Colaborador Obrigatório")]
    public int UserId { get; set; } = 0;
    public string? User { get; set; } = string.Empty;
    public int ManagerId { get; set; } = 0;
    public string Manager { get; set; } = string.Empty;
//  public bool? Prospection { get; set; } = false;
}
