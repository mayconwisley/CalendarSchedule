using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientContactDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Tipo obrigatório")]
    public string? Type { get; set; }

    [Required(ErrorMessage = "Numero obrigatório")]
    public string? Number { get; set; }

    [Required(ErrorMessage = "ClientId obrigatório")]
    public int ClientId { get; set; }
    public ClientDto? ClientDto { get; set; }

    [Required(ErrorMessage = "ResponsavelId obrigatório")]
    public int ClientResponsibleId { get; set; }
    public ClientResponsibleDto? ClientResponsibleDto { get; set; }
}
