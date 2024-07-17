using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class ClientResponsibleDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public bool Active { get; set; } = true;
    [JsonIgnore]
    public List<ClientContactDto>? ClientContactDtos { get; set; }
}
