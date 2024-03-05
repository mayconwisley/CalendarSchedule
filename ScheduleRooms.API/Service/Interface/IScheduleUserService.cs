using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface
{
    public interface IScheduleUserService
    {
        Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search);
        Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserId(int userId);
        Task<IEnumerable<ScheduleUserDto>> GetByScheduleActive();
        Task<IEnumerable<ScheduleUserDto>> GetByScheduleActiveUserId(int userId, DateTime dateSalected);
        Task<IEnumerable<ScheduleUserDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected);
        Task<IEnumerable<ScheduleUserDto>> GetByScheduleActiveClientIdUserId(int clientId, int userId, DateTime dateSalected);
        Task<ScheduleUserDto> GetById(int id);
        Task Create(ScheduleUserDto schedulesUserDto);
        Task Update(ScheduleUserDto schedulesUserDto);
        Task Delete(int id);
        Task<int> TotalSchedules(string search);
    }
}
