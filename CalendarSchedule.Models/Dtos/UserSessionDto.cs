namespace CalendarSchedule.Models.Dtos;

public class UserSessionDto
{
    public string? Username { get; set; } = string.Empty;
    public bool Manager { get; set; } = false;
}
