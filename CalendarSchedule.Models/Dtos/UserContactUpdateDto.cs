namespace CalendarSchedule.Models.Dtos;

public class UserContactUpdateDto
{
	public int Id { get; set; }
	public string Type { get; set; } = string.Empty;
	public string Number { get; set; } = string.Empty;
	public int UserId { get; set; }
}
