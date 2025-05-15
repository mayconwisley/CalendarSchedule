using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class ClientResponsibleCreateDto
{
	[Required(ErrorMessage = "Nome obrigatório")]
	[MaxLength(150)]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "E-mail obrigatório")]
	[MaxLength(150)]
	public string Email { get; set; } = string.Empty;

	[MaxLength(500)]
	public string? Description { get; set; } = string.Empty;

	[Required(ErrorMessage = "Cargo obrigatório")]
	[MaxLength(150)]
	public string Position { get; set; } = string.Empty;

	[Required]
	public bool Active { get; set; } = true;
}
