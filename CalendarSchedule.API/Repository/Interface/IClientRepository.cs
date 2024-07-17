using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAll(int page, int size, string search);
    Task<Client> GetById(int id);
    Task<Client> Create(Client client);
    Task<Client> Update(Client client);
    Task<Client> Delete(int id);
    Task<int> TotalClients(string search);
}
