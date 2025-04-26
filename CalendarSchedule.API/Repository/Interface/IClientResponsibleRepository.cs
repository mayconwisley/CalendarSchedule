using CalendarSchedule.API.Model;

namespace CalendarSchedule.API.Repository.Interface;

public interface IClientResponsibleRepository
{
    Task<IEnumerable<ClientResponsible>?> GetAll(int page, int size, string search);
    Task<ClientResponsible?> GetById(int id);
    Task<ClientResponsible> Create(ClientResponsible clientResponsible);
    Task<ClientResponsible?> Update(ClientResponsible clientResponsible);
    Task<ClientResponsible?> Delete(int id);
    Task<int> TotalClientResponsible(string search);

}
