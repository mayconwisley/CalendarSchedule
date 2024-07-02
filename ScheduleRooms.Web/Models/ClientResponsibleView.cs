using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Models;

public class ClientResponsibleView
{
    public int TotalData { get; set; }
    public int Page { get; set; }
    public int TotalPage { get; set; }
    public int Size { get; set; }
    public IEnumerable<ClientResponsibleDto>? ClientsResponsibleDto { get; set; }
}
