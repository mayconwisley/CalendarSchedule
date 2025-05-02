using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface ITokenStorageService
{
	Task<Result<TokenDto>> GetToken();
	Task<Result<TokenDto>> GetToken(LoginDto login);
	Task RemoverToken();

}
