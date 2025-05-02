using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IScheduleService
{
	Task<Result<PagedResult<ScheduleDto>>> GetAll(int page, int size, string search);
	Task<Result<ScheduleDto>> GetById(int id);
	Task<Result<ScheduleDto>> Create(ScheduleCreateDto scheduleCreateDto);
	Task<Result<ScheduleDto>> Update(ScheduleUpdateDto scheduleUpdateDto);
	Task<Result<ScheduleDto>> Delete(int id);
	Task<Result<int>> TotalSchedules(string search);
	Task<bool> ExistsOverlapCreate(ScheduleCreateDto scheduleCreateDto);
	Task<bool> ExistsOverlapUpdate(ScheduleUpdateDto scheduleUpdateDto);
	bool ValidateDatesCreate(ScheduleCreateDto scheduleCreateDto);
	bool ValidateDatesUpdate(ScheduleUpdateDto scheduleUpdateDto);
	bool IsParticularCreate(ScheduleCreateDto scheduleCreateDto);
	bool IsParticularUpdate(ScheduleUpdateDto scheduleUpdateDto);
}
