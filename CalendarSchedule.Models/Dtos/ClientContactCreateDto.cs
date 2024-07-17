using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientContactCreateDto
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
    public string? Type { get; set; }
    
    [Required]
    [MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
    public string? Number { get; set; }

    [Required]
    public int ClientId { get; set; }

    [Required]
    public int ClientResponsibleId { get; set; }
}
