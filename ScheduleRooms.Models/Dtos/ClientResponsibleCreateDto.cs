namespace ScheduleRooms.Models.Dtos;

public class ClientResponsibleCreateDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public string? Position { get; set; }
    public bool Active { get; set; }
}
