using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IUserContactService
{
    Task<IEnumerable<UserContactDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<UserContactDto>> GetByUserId(int page, int size, int userId);
    Task<UserContactDto> GetById(int id);
    Task<UserContactDto> Create(UserContactCreateDto userContactCreateDto);
    Task<UserContactDto> Update(UserContactCreateDto userContactCreateDto);
    Task Delete(int id);
    Task<int> TotalUserContact(string search);
}
