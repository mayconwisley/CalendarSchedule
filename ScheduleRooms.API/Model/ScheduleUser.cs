namespace ScheduleRooms.API.Model;

public class ScheduleUser
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
    public int UserId { get; set; }
    public virtual User? User { get; set; }
    public int ManagerId { get; set; }
    public string Manager { get; set; } = string.Empty;
}
