using AgendaSalas.Models.Dtos;

namespace AgendaSalas.Web.Models;

public class AgendaView
{
    public int TotalDados { get; set; }
    public int Pagina { get; set; }
    public int TotalPagina { get; set; }
    public int Tamanho { get; set; }
    public IEnumerable<AgendaDto>? Agendas { get; set; }
}
