using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ClientDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Responsável obrigatório")]
    public string? Responsible { get; set; }
    [Required(ErrorMessage = "Telefone Obrigatório")]
    [MaxLength(10, ErrorMessage = "Máximo 10 digito")]
    [MinLength(10, ErrorMessage = "Mínino 10 digito")]
    public string? Telephone { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
}
