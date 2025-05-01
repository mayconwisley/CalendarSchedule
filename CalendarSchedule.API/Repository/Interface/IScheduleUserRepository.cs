using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IScheduleUserRepository
{
	Task<IEnumerable<ScheduleUser>?> GetAll(int page, int size, string search);
	Task<IEnumerable<ScheduleUser>?> GetByScheduleId(int page, int size, string search, int scheduleId);
	Task<IEnumerable<ScheduleUser>?> GetByDateStart(int page, int size, string search, DateTime dateStart);
	Task<IEnumerable<ScheduleUser>?> GetByDatePeriod(int page, int size, string search, DateTime dateStart, DateTime dateEnd);
	Task<ScheduleUser?> GetById(int scheduleId, int userId);
	Task<ScheduleUser> Create(ScheduleUser scheduleUser);
	Task<ScheduleUser?> Delete(int scheduleId, int userId);
	Task<int> TotalScheduleUser(string search);
	Task<int> TotalScheduleUser(int scheduleId, string search);
	Task<int> TotalScheduleUser(DateTime dateStart, string search);
	Task<int> TotalScheduleUser(DateTime dateStart, DateTime dateEnd, string search);
}
