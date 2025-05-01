using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class UserContactDto
{
	public int Id { get; set; }
	public string Type { get; set; } = string.Empty;
	public string Number { get; set; } = string.Empty;
	[JsonIgnore]
	public int UserId { get; set; }
	public UserDto UserDto { get; set; } = new();
}
