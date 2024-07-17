using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface ILoginService
{
    public Task<TokenDto> Token(LoginDto login);
  
}
