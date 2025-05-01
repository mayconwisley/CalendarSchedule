using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleUserService
{
	Task<Result<PagedResult<ScheduleUserDto>>> GetAll(int page, int size, string search);
	Task<Result<PagedResult<ScheduleUserDto>>> GetByScheduleId(int page, int size, string search, int scheduleId);
	Task<Result<PagedResult<ScheduleUserDto>>> GetByDateStart(int page, int size, string search, DateOnly dateStart);
	Task<Result<PagedResult<ScheduleUserDto>>> GetByDatePeriod(int page, int size, string search, DateOnly dateStart, DateOnly dateEnd);
	Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId);
	Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto);

	Task<Result<ScheduleUserDto>> Delete(int scheduleId, int userId);
	Task<Result<int>> TotalScheduleUser(string search);
	Task<Result<int>> TotalScheduleUser(int scheduleId, string search);
	Task<Result<int>> TotalScheduleUser(DateTime dateStart, string search);
	Task<Result<int>> TotalScheduleUser(DateTime dateStart, DateTime dateEnd, string search);
}
