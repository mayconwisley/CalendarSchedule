using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaSalas.Models
{
    public class Reuniao
    {
        public int Id { get; set; }
        [Column(TypeName = "VARCHAR(1000)")]
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        [Required]
        public DateTime DataFim { get; set; }
        public bool PermitirLigar { get; set; }
        public bool PermitirChamar { get; set; }
        public int SalaId { get; set; }
        public Sala Sala { get; set; }
    }
}
