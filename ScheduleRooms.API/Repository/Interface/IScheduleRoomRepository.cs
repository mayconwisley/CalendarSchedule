using ScheduleRooms.API.Model;

namespace ScheduleRooms.API.Repository.Interface;

public interface IScheduleRoomRepository
{
    Task<IEnumerable<ScheduleRoom>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleRoom>> GetByAgendaActive();
    Task<IEnumerable<ScheduleRoom>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected);
    Task<ScheduleRoom> GetById(int id);
    Task<ScheduleRoom> Create(ScheduleRoom schedule);
    Task<ScheduleRoom> Update(ScheduleRoom schedule);
    Task<ScheduleRoom> Delete(int id);
    Task<int> TotalSchedules(string search);
}
