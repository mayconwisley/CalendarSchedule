using ScheduleRooms.API.Model;

namespace ScheduleRooms.API.Repository.Interface;

public interface IClientContactRepository
{

    Task<IEnumerable<ClientContact>> GetAll(int page, int size, string search);
    Task<IEnumerable<ClientContact>> GetByClientId(int page, int size, int clientId);
    Task<ClientContact> GetById(int id);
    Task<ClientContact> Create(ClientContact clientContact);
    Task<ClientContact> Update(ClientContact clientContact);
    Task<ClientContact> Delete(int id);
    Task<int> TotalClientContact(string search);
}
