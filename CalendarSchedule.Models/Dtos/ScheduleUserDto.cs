using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class ScheduleUserDto
{
	[JsonIgnore]
	public int UserId { get; set; }
	public UserDto? UserDto { get; set; }
	[JsonIgnore]
	public int ScheduleId { get; set; }
	public ScheduleDto? ScheduleDto { get; set; }
}
