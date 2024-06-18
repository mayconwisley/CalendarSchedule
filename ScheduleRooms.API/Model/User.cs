namespace ScheduleRooms.API.Model;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool Manager { get; set; }
    public bool Active { get; set; }
    public List<ScheduleUser>? ScheduleUsers { get; set; }
    public List<UserContact>? UserContacts { get; set; }
}
