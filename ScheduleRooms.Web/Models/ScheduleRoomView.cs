using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Models;

public class ScheduleRoomView
{
    public int TotalData { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
    public int Size { get; set; }
    public IEnumerable<ScheduleRoomDto>? SchedulesDto { get; set; }
}
