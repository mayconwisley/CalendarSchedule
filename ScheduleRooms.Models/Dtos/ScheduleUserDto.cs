namespace ScheduleRooms.Models.Dtos;

public class ScheduleUserDto
{
    public int UserId { get; set; }
    public UserDto User { get; set; }
    public int ScheduleId { get; set; }
    public ScheduleDto Schedule { get; set; }
}
