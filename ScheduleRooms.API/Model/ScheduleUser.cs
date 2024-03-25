namespace ScheduleRooms.API.Model;

public class ScheduleUser
{
    public int Id { get; set; } = 0;
    public string? Description { get; set; } = string.Empty;
    public DateTime DateStart { get; set; } = DateTime.Now;
    public DateTime DateFinal { get; set; } = DateTime.Now;   
    public bool AllowCall { get; set; } = false;
    public bool AllowChat { get; set; } = false;            
    public int ClientId { get; set; } = 0;
    public virtual Client? Client { get; set; } = new();
    public int UserId { get; set; } = 0;
    public virtual User? User { get; set; } = new();
}
