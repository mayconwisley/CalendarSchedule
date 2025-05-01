using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class ScheduleDto
{
	public int Id { get; set; }
	public string? Description { get; set; }
	public DateTime DateStart { get; set; } = DateTime.Now;
	public DateTime DateFinal { get; set; } = DateTime.Now.AddHours(1);
	public bool MeetingType { get; set; } = false;
	public bool StatusSchedule { get; set; } = false;
	public bool Particular { get; set; } = false;
	[JsonIgnore]
	public int? ClientId { get; set; }
	public ClientDto? ClientDto { get; set; }
}
