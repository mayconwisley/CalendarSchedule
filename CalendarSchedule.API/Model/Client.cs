namespace CalendarSchedule.API.Model;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Telephone { get; set; }
    public string? State { get; set; }
    public string? City { get; set; }
    public string? Road { get; set; }
    public string? Number { get; set; }
    public string? Garden { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
    public bool Prospection { get; set; }
    public ICollection<Schedule>? ScheduleUsers { get; set; }

}
