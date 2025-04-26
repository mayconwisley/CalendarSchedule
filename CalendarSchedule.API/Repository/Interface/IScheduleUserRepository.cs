using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IScheduleUserRepository
{
    Task<IEnumerable<ScheduleUser>?> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleUser>?> GetByScheduleId(int scheduleId);
    Task<IEnumerable<ScheduleUser>?> GetByDateStart(DateTime dateStart);
    Task<IEnumerable<ScheduleUser>?> GetByDatePeriod(DateTime dateStart, DateTime dateEnd);
    Task<ScheduleUser?> GetById(int scheduleId, int userId);
    Task<ScheduleUser> Create(ScheduleUser scheduleUser);
    Task<ScheduleUser?> Update(ScheduleUser scheduleUser);
    Task<ScheduleUser?> Delete(int scheduleId, int userId);
    Task<int> TotalScheduleUser(string search);
}
