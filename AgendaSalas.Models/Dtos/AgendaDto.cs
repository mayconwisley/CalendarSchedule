using System.ComponentModel.DataAnnotations;

namespace AgendaSalas.Models.Dtos;

public class AgendaDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Descrição Obrigatório")]
    public string? Descricao { get; set; }
    [Required(ErrorMessage = "Data Inicio Obrigatório")]
    public DateTime DataInicio { get; set; } = DateTime.Now;
    [Required(ErrorMessage = "Data Final Obrigatório")]
    public DateTime DataFinal { get; set; } = DateTime.Now.AddHours(1);
    public bool PermitirLigar { get; set; } = false;
    public bool PermitirChamar { get; set; } = false;
    [Required(ErrorMessage = "Sala Obrigatória")]
    public int SalaId { get; set; }
    public string? Sala { get; set; }
}
