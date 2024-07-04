namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserDto
{
    public int UserId { get; set; }
    public UserDto? UserDto { get; set; }
    public int ScheduleId { get; set; }
    public ScheduleDto? ScheduleDto { get; set; }
}
