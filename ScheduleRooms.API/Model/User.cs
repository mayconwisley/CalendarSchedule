namespace ScheduleRooms.API.Model;

public class User
{
    public int Id { get; set; } = 0;
    public string? Name { get; set; } = string.Empty;
    public string? Cellphone { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool Active { get; set; } = true;
}
