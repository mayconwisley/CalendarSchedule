using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public string? Username { get; set; } = string.Empty;
    [JsonIgnore]
    public string? Password { get; set; } = string.Empty;
    public bool? Manager { get; set; } = false;
    public bool? Active { get; set; } = true;
}
