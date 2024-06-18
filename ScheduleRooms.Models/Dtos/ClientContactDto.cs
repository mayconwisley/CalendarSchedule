namespace ScheduleRooms.Models.Dtos;

public class ClientContactDto
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public int ClientId { get; set; }
    public ClientDto? ClientDto { get; set; }
    public int ClientResponsibleId { get; set; }
    public ClientResponsibleDto? ClientResponsibleDto { get; set; }
}
