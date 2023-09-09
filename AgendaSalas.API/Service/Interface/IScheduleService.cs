using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleDto>> GetByAgendaActive();
    Task<IEnumerable<ScheduleDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected);
    Task<ScheduleDto> GetById(int id);
    Task Create(ScheduleDto scheduleDto);
    Task Update(ScheduleDto scheduleDto);
    Task Delete(int id);
    Task<int> TotalAgendas(string search);
}
