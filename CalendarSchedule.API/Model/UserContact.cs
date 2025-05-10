namespace CalendarSchedule.API.Model;

public class UserContact
{
	public int Id { get; set; }
	public string Type { get; set; } = string.Empty;
	public string Number { get; set; } = string.Empty;
	public int UserId { get; set; }
	public User? User { get; set; }
}
