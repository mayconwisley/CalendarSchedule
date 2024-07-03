using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IScheduleService
{
    Task<ScheduleView> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleDto>> GetBySchedule();
    Task<IEnumerable<ScheduleDto>> GetByScheduleActive();
    Task<IEnumerable<ScheduleDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected);
    Task<ScheduleDto> GetById(int id);
    Task<ScheduleDto> Create(ScheduleCreateDto scheduleCreateDto);
    Task<ScheduleDto> Update(ScheduleCreateDto scheduleCreateDto);
    Task<bool> Delete(int id);
    Task<int> TotalSchedules(string search);
}
