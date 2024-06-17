using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleDto>> GetBySchedule();
    Task<IEnumerable<ScheduleDto>> GetByScheduleActive();
    Task<IEnumerable<ScheduleDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected);
    Task<ScheduleDto> GetById(int id);
    Task<ScheduleDto> Create(ScheduleDto scheduleDto);
    Task<ScheduleDto> Update(ScheduleDto scheduleDto);
    Task Delete(int id);
    Task<int> TotalSchedules(string search);
}
