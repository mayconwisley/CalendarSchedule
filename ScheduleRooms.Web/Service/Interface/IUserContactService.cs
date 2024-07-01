using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IUserContactService
{
    Task<UserView> GetAll(int page = 1, int size = 10, string search = "");
    Task<UserContactDto> GetById(int id);
    Task<UserContactView> GetByUserId(int page = 1, int size = 10, int userId = 0);
    Task<UserContactDto> Create(UserContactCreateDto userContactCreateDto);
    Task<UserContactDto> Update(UserContactCreateDto userContactCreateDto);
    Task<bool> Delete(int id);
}
