using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IScheduleUserService
{
    Task<Result<PagedResultView<ScheduleUserDto>>> GetAll(int page = 1, int size = 10, string search = "");
    Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleUserDateStart(DateTime dateSelected);
    Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleUserDatePeriod(DateTime dateStart, DateTime dateEnd);
    Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId);
    Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleId(int scheduleId);
    Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserDto);
    Task<Result<ScheduleUserDto>> Update(ScheduleUserCreateDto scheduleUserDto);
    Task<Result<bool>> Delete(int scheduleId, int userId);
}
