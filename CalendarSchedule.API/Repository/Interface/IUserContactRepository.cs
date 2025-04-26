using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IUserContactRepository
{
    Task<IEnumerable<UserContact>> GetAll(int page, int size, string search);
    Task<IEnumerable<UserContact>> GetByUserId(int page, int size, int userId);
    Task<UserContact?> GetById(int id);
    Task<UserContact> Create(UserContact userContact);
    Task<UserContact?> Update(UserContact userContact);
    Task<UserContact?> Delete(int id);
    Task<int> TotalUserContact(string search);
}
