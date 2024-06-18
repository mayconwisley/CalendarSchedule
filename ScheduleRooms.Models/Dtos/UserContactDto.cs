namespace ScheduleRooms.Models.Dtos;

public class UserContactDto
{
    public int Id { get; set; }
    public string? Type { get; set; }
    public string? Number { get; set; }
    public int UserId { get; set; }
    public UserDto? UserDto { get; set; }
}
