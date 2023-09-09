using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class RoomDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name da Room é Obrigatório")]
    public string? Name { get; set; }
    public string? Ramal { get; set; }
    public string? Description { get; set; }
}
