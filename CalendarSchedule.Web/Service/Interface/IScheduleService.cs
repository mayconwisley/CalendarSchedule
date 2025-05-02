using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IScheduleService
{
    Task<Result<PagedResultView<ScheduleDto>>> GetAll(int page, int size, string search);
    Task<Result<PagedResultView<ScheduleDto>>> GetBySchedule();
    Task<Result<PagedResultView<ScheduleDto>>> GetByScheduleActive();
    Task<Result<PagedResultView<ScheduleDto>>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected);
    Task<Result<ScheduleDto>> GetById(int id);
    Task<Result<ScheduleDto>> Create(ScheduleCreateDto scheduleCreateDto);
    Task<Result<ScheduleDto>> Update(ScheduleUpdateDto scheduleUpdateDto);
    Task<Result<bool>> Delete(int id);
    Task<Result<int>> TotalSchedules(string search);
}
