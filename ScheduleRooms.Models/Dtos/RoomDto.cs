using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class RoomDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; } = string.Empty;
    public string? Ramal { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
