using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IUserContactService
{
    Task<Result<IEnumerable<UserContactDto>>> GetAll(int page, int size, string search);
    Task<Result<IEnumerable<UserContactDto>>> GetByUserId(int page, int size, int userId);
    Task<Result<UserContactDto>> GetById(int id);
    Task<Result<UserContactDto>> Create(UserContactCreateDto userContactCreateDto);
    Task<Result<UserContactDto>> Update(UserContactCreateDto userContactCreateDto);
    Task<Result> Delete(int id);
    Task<Result<int>> TotalUserContact(string search);
}
