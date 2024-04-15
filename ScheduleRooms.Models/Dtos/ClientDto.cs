using System.ComponentModel.DataAnnotations;
using System.IO.Pipes;

namespace ScheduleRooms.Models.Dtos;

public class ClientDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Responsável obrigatório")]
    public string? Responsible { get; set; } = string.Empty;
    public string? Position { get; set; }
    [Required(ErrorMessage = "Telefone Obrigatório")]
    [MaxLength(14, ErrorMessage = "Máximo 14 digito")]
    [MinLength(10, ErrorMessage = "Mínino 10 digito")]
    public string? Telephone { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
}
