using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class UserDto
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
	public string? Username { get; set; }
	[JsonIgnore]
	public string? Password { get; set; }
	public bool? Manager { get; set; }
	public bool? Active { get; set; }
}
