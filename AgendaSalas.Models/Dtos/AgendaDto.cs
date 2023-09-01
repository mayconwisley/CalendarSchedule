using System.ComponentModel.DataAnnotations;

namespace AgendaSalas.Models.Dtos;

public class AgendaDto
{
    public int Id { get; set; }
    [Required]
    public string? Descricao { get; set; }
    [Required]
    public DateTime DataInicio { get; set; } = DateTime.Now;
    [Required]
    public DateTime DataFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool PermitirLigar { get; set; } = false;
    public bool PermitirChamar { get; set; } = false;
    [Required]
    public int SalaId { get; set; }
    public string? Sala { get; set; }
}
