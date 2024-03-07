using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IScheduleUserService
{
    Task<ScheduleUserView> GetAll(int page = 1, int size = 10, string search = "");
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserId(int userId);
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUser();
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActive();
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveUserId(int userId, DateTime dateSelected);
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveClientId(int clientId, DateTime dateSelected);
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveClinetIdUserId(int clientId, int userId, DateTime dateSelected);
    Task<ScheduleUserDto> GetById(int id);
    Task<ScheduleUserDto> Create(ScheduleUserDto scheduleUserDto);
    Task<ScheduleUserDto> Update(ScheduleUserDto scheduleUserDto);
    Task<bool> Delete(int id);
}
