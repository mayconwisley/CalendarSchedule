using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleDto>> GetAll(int page, int size, string search);
    Task<ScheduleDto> GetById(int id);
    Task<ScheduleDto> Create(ScheduleCreateDto scheduleCreateDto);
    Task<ScheduleDto> Update(ScheduleCreateDto scheduleCreateDto);
    Task Delete(int id);
    Task<int> TotalSchedules(string search);
}
