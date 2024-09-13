using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleUserService
{
    Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleId(int scheduleId);
    Task<IEnumerable<ScheduleUserDto>> GetByDateStart(DateTime dateStart);
    Task<IEnumerable<ScheduleUserDto>> GetByDatePeriod(DateTime dateStart, DateTime dateEnd);
    Task<ScheduleUserDto> GetById(int scheduleId, int userId);
    Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserCreateDto);
    Task<ScheduleUserDto> Update(ScheduleUserCreateDto scheduleUserCreateDto);
    Task Delete(int scheduleId, int userId);
    Task<int> TotalScheduleUser(string search);
}
