using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Celular obrigatório")]
    public string? Cellphone { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
}
