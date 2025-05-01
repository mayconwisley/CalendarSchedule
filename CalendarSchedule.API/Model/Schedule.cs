namespace CalendarSchedule.API.Model;

public class Schedule
{
	public int Id { get; set; }
	public string? Description { get; set; }
	public DateTime DateStart { get; set; }
	public DateTime DateFinal { get; set; }
	public bool MeetingType { get; set; }
	public bool StatusSchedule { get; set; }
	public bool Particular { get; set; }
	public int? ClientId { get; set; }
	public Client? Client { get; set; }
	public List<ScheduleUser>? ScheduleUsers { get; set; }
}
