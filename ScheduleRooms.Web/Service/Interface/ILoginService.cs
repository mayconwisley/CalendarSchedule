using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Service.Interface;

public interface ILoginService
{
    public Task<string> Token(LoginDto login);
}
