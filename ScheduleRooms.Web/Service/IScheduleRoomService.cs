using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service;

public interface IScheduleRoomService
{
    Task<ScheduleRoomView> GetAll(int page = 1, int size = 10, string search = "");
    Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActive();
    Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected);
    Task<ScheduleRoomDto> GetById(int id);
    Task<ScheduleRoomDto> Create(ScheduleRoomDto scheduleDto);
    Task<ScheduleRoomDto> Update(ScheduleRoomDto scheduleDto);
    Task<bool> Delete(int id);
}
