using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IScheduleRoomService
{
    Task<IEnumerable<ScheduleRoomDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActive();
    Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected);
    Task<ScheduleRoomDto> GetById(int id);
    Task Create(ScheduleRoomDto scheduleDto);
    Task Update(ScheduleRoomDto scheduleDto);
    Task Delete(int id);
    Task<int> TotalAgendas(string search);
}
