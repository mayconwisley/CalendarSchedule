namespace ScheduleRooms.Models.Dtos;

public class ClientContactCreateDto
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public int ClientId { get; set; }
    public int ClientResponsibleId { get; set; }
}
