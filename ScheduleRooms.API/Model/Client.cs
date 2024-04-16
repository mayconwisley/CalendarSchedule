namespace ScheduleRooms.API.Model;

public class Client
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Responsible { get; set; }
    public string? Position { get; set; }
    public string? Telephone { get; set; }
    public string? Email { get; set; }
    public string? City { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
    public bool Prospection { get; set; }

}
