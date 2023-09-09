using ScheduleRooms.API.Model;

namespace ScheduleRooms.API.Repository.Interface;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAll(int page, int size, string search);
    Task<Room> GetById(int id);
    Task<Room> Create(Room room);
    Task<Room> Update(Room room);
    Task<Room> Delete(int id);
    Task<int> TotalRooms(string search);
}
