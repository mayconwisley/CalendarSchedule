using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.Web.Service.Interface;

public interface ILoginService
{
    public Task<TokenDto> Token(LoginDto login);
  
}
