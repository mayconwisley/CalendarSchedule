namespace ScheduleRooms.API.Model;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; } = string.Empty;
    public string? Responsible { get; set; } = string.Empty;
    public string? Telephone { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public bool Active { get; set; } = true;

}
