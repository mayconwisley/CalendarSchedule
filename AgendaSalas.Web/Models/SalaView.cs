using AgendaSalas.Models.Dtos;

namespace AgendaSalas.Web.Models;

public class SalaView
{
    public int TotalDados { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
    public int Size { get; set; }
    public IEnumerable<SalaDto>? SalasDto { get; set; }
}
