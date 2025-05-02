using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IUserContactService
{
    Task<Result<PagedResultView<UserDto>>> GetAll(int page = 1, int size = 10, string search = "");
    Task<Result<UserContactDto>> GetById(int id);
    Task<Result<PagedResultView<UserContactDto>>> GetByUserId(int page = 1, int size = 10, int userId = 0);
    Task<Result<UserContactDto>> Create(UserContactCreateDto userContactCreateDto);
    Task<Result<UserContactDto>> Update(UserContactDto userContactDto);
    Task<Result<bool>> Delete(int id);
}
