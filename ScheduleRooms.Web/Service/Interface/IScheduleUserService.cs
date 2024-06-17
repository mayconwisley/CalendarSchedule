using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IScheduleUserService
{
    Task<ScheduleUserView> GetAll(int page = 1, int size = 10, string search = "");
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserId(int userId);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUser();
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActive();
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserDateUserId(int userId, DateTime dateSelected);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClientId(int clientId, DateTime dateSelected);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClinetIdUserId(int clientId, int userId, DateTime dateSelected);
    Task<ScheduleDto> GetById(int id);
    Task<ScheduleDto> Create(ScheduleDto scheduleUserDto);
    Task<ScheduleDto> Update(ScheduleDto scheduleUserDto);
    Task<bool> Delete(int id);
}
