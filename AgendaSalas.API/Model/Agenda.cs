namespace AgendaSalas.API.Model;

public class Agenda
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public bool PermitirLigar { get; set; }
    public bool PermitirChamar { get; set; }
    public int SalaId { get; set; }
    public virtual Sala? Sala { get; set; }
}
