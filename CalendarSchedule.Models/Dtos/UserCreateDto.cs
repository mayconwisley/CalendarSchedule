using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class UserCreateDto
{
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Usuário Obrigatório")]
    [MaxLength(16, ErrorMessage = "Máximo 16 digito")]
    public string? Username { get; set; } = string.Empty;
    [Required(ErrorMessage = "Senha Obrigatória")]
    public string? Password { get; set; } = string.Empty;
    public bool Manager { get; set; } = false;
    public bool Active { get; set; } = true;
}
