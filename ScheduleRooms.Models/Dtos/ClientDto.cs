using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ClientDto
{
    public int Id { get; set; } = 0;
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Telefone Obrigatório")]
    [MaxLength(14, ErrorMessage = "Máximo 14 digito")]
    [MinLength(10, ErrorMessage = "Mínino 10 digito")]
    public string? Telephone { get; set; } = string.Empty;
    [Required(ErrorMessage = "Responsável obrigatório")]
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Road { get; set; }
    public string? Number { get; set; }
    public string? Garden { get; set; }
    public string? Description { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
    public bool Prospection { get; set; } = false;
}
