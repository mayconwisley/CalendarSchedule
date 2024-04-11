namespace ScheduleRooms.API.Model;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Cellphone { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
}
