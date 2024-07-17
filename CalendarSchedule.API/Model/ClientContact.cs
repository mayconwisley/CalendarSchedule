namespace CalendarSchedule.API.Model;

public class ClientContact
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    public int ClientResponsibleId { get; set; }
    public ClientResponsible? ClientResponsible { get; set; }
}
