using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CalendarSchedule.Models.Dtos;

public class UserContactDto
{

	public int Id { get; set; }
	[Required(ErrorMessage = "Tipo Obrigatório")]
	public string Type { get; set; } = string.Empty;
	[Required(ErrorMessage = "O número é obrigatório")]
	[MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
	[RegularExpression(@"^[0-9()\-\s]+$", ErrorMessage = "Número inválido. Use apenas números, espaços, parênteses e hífens.")]
	public string Number { get; set; } = string.Empty;
	[JsonIgnore]
	public int UserId { get; set; }
	public UserDto UserDto { get; set; } = new();
}
