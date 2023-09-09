using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IRoomService
{
    Task<IEnumerable<RoomDto>> GetAll(int page, int size, string search);
    Task<RoomDto> GetById(int id);
    Task Create(RoomDto roomDto);
    Task Update(RoomDto roomDto);
    Task Delete(int id);
    Task<int> TotalSalas(string search);
}
