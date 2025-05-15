using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientContactCreateDto
{
	[Required(ErrorMessage = "Tipo obrigatório")]
	[MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
	public string Type { get; set; } = string.Empty;

	[Required(ErrorMessage = "Número obrigatório")]
	[MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
	[RegularExpression(@"^[0-9()\-\s]+$", ErrorMessage = "Número inválido. Use apenas números, espaços, parênteses e hífens.")]
	public string Number { get; set; } = string.Empty;

	[Required]
	public int ClientId { get; set; }

	[Required]
	public int ClientResponsibleId { get; set; }
}
