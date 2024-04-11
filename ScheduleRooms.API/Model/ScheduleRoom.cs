namespace ScheduleRooms.API.Model;

public class ScheduleRoom
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DateTime DateStart { get; set; } 
    public DateTime DateFinal { get; set; } 
    public bool AllowCall { get; set; }
    public bool AllowChat { get; set; }
    public int RoomId { get; set; }
    public virtual Room? Room { get; set; }
}
