using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ScheduleUpdateDto
{
	[Required(ErrorMessage = "Id Obrigatório")]
	public int Id { get; set; }
	[Required(ErrorMessage = "Data Inicio Obrigatório")]
	public DateTime DateStart { get; set; } = DateTime.Now;
	[Required(ErrorMessage = "Data Final Obrigatório")]
	public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
	public string? Description { get; set; } = null;
	public bool MeetingType { get; set; } = false;
	public bool StatusSchedule { get; set; } = false;
	public bool Particular { get; set; } = false;
	public int? ClientId { get; set; }
}
