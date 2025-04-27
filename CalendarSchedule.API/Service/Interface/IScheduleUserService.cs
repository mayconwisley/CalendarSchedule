using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleUserService
{
    Task<Result<IEnumerable<ScheduleUserDto>>> GetAll(int page, int size, string search);
    Task<Result<IEnumerable<ScheduleUserDto>>> GetByScheduleId(int scheduleId);
    Task<Result<IEnumerable<ScheduleUserDto>>> GetByDateStart(DateTime dateStart);
    Task<Result<IEnumerable<ScheduleUserDto>>> GetByDatePeriod(DateTime dateStart, DateTime dateEnd);
    Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId);
    Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto);
    Task<Result<ScheduleUserDto>> Update(ScheduleUserCreateDto scheduleUserCreateDto);
    Task<Result> Delete(int scheduleId, int userId);
    Task<Result<int>> TotalScheduleUser(string search);
}
