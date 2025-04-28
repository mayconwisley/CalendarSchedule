using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleUserService
{
    Task<Result<PagedResult<ScheduleUserDto>>> GetAll(int page, int size, string search);
    Task<Result<PagedResult<ScheduleUserDto>>> GetByScheduleId(int page, int size, string search, int scheduleId);
    Task<Result<PagedResult<ScheduleUserDto>>> GetByDateStart(int page, int size, string search, DateTime dateStart);
    Task<Result<PagedResult<ScheduleUserDto>>> GetByDatePeriod(int page, int size, string search, DateTime dateStart, DateTime dateEnd);
    Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId);
    Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto);
    Task<Result<ScheduleUserDto>> Update(ScheduleUserDto scheduleUserDto);
    Task<Result<ScheduleUserDto>> Delete(int scheduleId, int userId);
    Task<Result<int>> TotalScheduleUser(string search);
}
