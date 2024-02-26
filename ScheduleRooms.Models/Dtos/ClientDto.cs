using System.ComponentModel.DataAnnotations;

namespace ScheduleRooms.Models.Dtos;

public class ClientDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome obrigatório")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Responsavel obrigatório")]
    public string? Responsible { get; set; }
    [Required(ErrorMessage = "Telefone Obrigatório")]
    public string? Telephone { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
}
