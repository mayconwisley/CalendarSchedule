namespace CalendarSchedule.API.Model;

public class ScheduleUser
{
    public int UserId { get; set; }
    public User? User { get; set; }
    public int ScheduleId { get; set; }
    public Schedule? Schedule { get; set; }
}
