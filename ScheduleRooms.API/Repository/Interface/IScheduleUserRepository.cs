using ScheduleRooms.API.Model;

namespace ScheduleRooms.API.Repository.Interface;

public interface IScheduleUserRepository
{
    Task<IEnumerable<ScheduleUser>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleUser>> GetByScheduleActive();
    Task<IEnumerable<ScheduleUser>> GetByScheduleActiveUserId(int userId, DateTime dateSalected);
    Task<IEnumerable<ScheduleUser>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected);
    Task<IEnumerable<ScheduleUser>> GetByScheduleActiveClientIdUserId(int clientId, int userId, DateTime dateSalected);
    Task<ScheduleUser> GetById(int id);
    Task<ScheduleUser> Create(ScheduleUser schedulesUser);
    Task<ScheduleUser> Update(ScheduleUser schedulesUser);
    Task<ScheduleUser> Delete(int id);
    Task<int> TotalSchedules(string search);
}
