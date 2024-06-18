namespace ScheduleRooms.API.Model;

public class ClientResponsible
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public bool Active { get; set; }
    public List<ClientContact> ClientContacts { get; set; }
}
