namespace ScheduleRooms.API.Model;

public class Room
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Ramal { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}
