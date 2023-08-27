using System.ComponentModel.DataAnnotations;

namespace AgendaSalas.Models.Dtos;

public class SalaDto
{
    public int Id { get; set; }
    [Required]
    public string? Nome { get; set; }
    public string? Ramal { get; set; }
    public string? Descricao { get; set; }
}
