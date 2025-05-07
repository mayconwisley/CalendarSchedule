using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.Web.Service.Interface;

public interface ILoginService
{
    public Task<Result<TokenDto>> Token(LoginDto login);

}
