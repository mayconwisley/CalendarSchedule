using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service;

public interface IRoomService
{
    Task<RoomView> GetAll(int page = 1, int size = 10, string search = "");
    Task<RoomDto> GetById(int id);
    Task<RoomDto> Create(RoomDto roomDto);
    Task<RoomDto> Update(RoomDto roomDto);
    Task<bool> Delete(int id);
}
