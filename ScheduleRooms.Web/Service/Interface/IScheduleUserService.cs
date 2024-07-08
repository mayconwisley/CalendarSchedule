using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IScheduleUserService
{
    Task<ScheduleUserView> GetAll(int page = 1, int size = 10, string search = "");
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserId(int userId);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUser();
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActive();
    Task<ScheduleUserView> GetByScheduleUserDateStart(DateTime dateSelected);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClientId(int clientId, DateTime dateSelected);
    Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClinetIdUserId(int clientId, int userId, DateTime dateSelected);
    Task<ScheduleUserDto> GetById(int scheduleId, int userId);
    Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserDto);
    Task<ScheduleUserDto> Update(ScheduleUserCreateDto scheduleUserDto);
    Task<bool> Delete(int scheduleId, int userId);
}
