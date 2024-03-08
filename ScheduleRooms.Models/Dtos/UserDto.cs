using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Celular obrigatório")]
    [MaxLength(10, ErrorMessage = "Máximo 11 digito")]
    [MinLength(10, ErrorMessage = "Mínino 11 digito")]
    public string? Cellphone { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
}
