using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class ClientContactDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Tipo obrigatório")]
    public string Type { get; set; } = string.Empty;

    [Required(ErrorMessage = "Numero obrigatório")]
    public string Number { get; set; } = string.Empty;

    [Required(ErrorMessage = "ClientId obrigatório")]
    [JsonIgnore]
    public int ClientId { get; set; }
    public ClientDto? ClientDto { get; set; }

    [Required(ErrorMessage = "ResponsavelId obrigatório")]
    [JsonIgnore]
    public int ClientResponsibleId { get; set; }
    public ClientResponsibleDto? ClientResponsibleDto { get; set; }
}
