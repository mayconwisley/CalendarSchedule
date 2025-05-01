using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.Models.Dtos;

public class UserContactCreateDto
{
	[Required]
	[MaxLength(50)]
	public string Type { get; set; } = string.Empty;

	[Required]
	[MaxLength(20)]
	public string Number { get; set; } = string.Empty;

	[Required]
	public int UserId { get; set; }
}
