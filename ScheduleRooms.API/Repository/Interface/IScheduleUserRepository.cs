using ScheduleRooms.API.Model;

namespace ScheduleRooms.API.Repository.Interface;

public interface IScheduleUserRepository
{
    Task<IEnumerable<ScheduleUser>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleUser>> GetByScheduleId(int scheduleId);
    Task<IEnumerable<ScheduleUser>> GetByDateStart(int page, int size, DateTime dateStart);
    Task<ScheduleUser> GetById(int userId, int scheduleId);
    Task<ScheduleUser> Create(ScheduleUser scheduleUser);
    Task<ScheduleUser> Update(ScheduleUser scheduleUser);
    Task<IEnumerable<ScheduleUser>> Delete(int scheduleId);
    Task<int> TotalScheduleUser(string search);
}
