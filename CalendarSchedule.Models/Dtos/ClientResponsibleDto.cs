using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class ClientResponsibleDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public string? Description { get; set; } = string.Empty;
	public string Position { get; set; } = string.Empty;
	public bool Active { get; set; } = true;
	[JsonIgnore]
	public List<ClientContactDto>? ClientContactDtos { get; set; }
}
