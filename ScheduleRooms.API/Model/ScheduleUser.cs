namespace ScheduleRooms.API.Model;

public class ScheduleUser
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DateTime DateStart { get; set; } 
    public DateTime DateFinal { get; set; } 
    public bool AllowCall { get; set; }
    public bool AllowChat { get; set; }
    public int ClientId { get; set; }
    public virtual Client? Client { get; set; }
    public int UserId { get; set; }
    public virtual User? User { get; set; }
}
