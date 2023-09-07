using System.ComponentModel.DataAnnotations;

namespace AgendaSalas.Models.Dtos;

public class SalaDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome da Sala é Obrigatório")]
    public string? Nome { get; set; }
    public string? Ramal { get; set; }
    public string? Descricao { get; set; }
}
