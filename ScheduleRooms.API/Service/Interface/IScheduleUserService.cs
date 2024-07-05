using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IScheduleUserService
{
    Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ScheduleUserDto>> GetByScheduleId(int scheduleId);
    Task<IEnumerable<ScheduleUserDto>> GetByDateStart(int page, int size, DateTime dateStart);
    Task<ScheduleUserDto> GetById(int scheduleId, int userId);
    Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserCreateDto);
    Task<ScheduleUserDto> Update(ScheduleUserCreateDto scheduleUserCreateDto);
    Task Delete(int scheduleId, int userId);
    Task<int> TotalScheduleUser(string search);
}
