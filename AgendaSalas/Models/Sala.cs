using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaSalas.Models;

public class Sala
{
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(500)")]
    public string SalaReuniao { get; set; }
    [Column(TypeName = "VARCHAR(5)")]
    public string Ramal { get; set; }
    [Column(TypeName = "VARCHAR(1000)")]
    public string Descricao { get; set; }
}
