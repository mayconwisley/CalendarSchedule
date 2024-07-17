using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Models;

public class ScheduleUserView
{
    public int TotalData { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
    public int Size { get; set; }
    public IEnumerable<ScheduleUserDto>? ScheduleUsersDto { get; set; }
}
