using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service;

public interface IScheduleService
{
    Task<ScheduleView> GetAll(int page = 1, int size = 10, string search = "");
    Task<IEnumerable<ScheduleDto>> GetByAgendaActive();
    Task<IEnumerable<ScheduleDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected);
    Task<ScheduleDto> GetById(int id);
    Task<ScheduleDto> Create(ScheduleDto scheduleDto);
    Task<ScheduleDto> Update(ScheduleDto scheduleDto);
    Task<bool> Delete(int id);
}
