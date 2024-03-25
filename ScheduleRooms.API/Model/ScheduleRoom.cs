namespace ScheduleRooms.API.Model;

public class ScheduleRoom
{
    public int Id { get; set; } = 0;
    public string? Description { get; set; } = string.Empty;
    public DateTime DateStart { get; set; } = DateTime.Now;
    public DateTime DateFinal { get; set; } = DateTime.Now;
    public bool AllowCall { get; set; } = false;
    public bool AllowChat { get; set; } = false;
    public int RoomId { get; set; } = 0;
    public virtual Room? Room { get; set; } = new();
}
