using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface ITokenStorageService
{
    Task<TokenDto> GetToken();
    Task<TokenDto> GetToken(LoginDto login);
    Task RemoverToken();
  
}
