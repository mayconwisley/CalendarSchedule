using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IClientContactService
{
    Task<ClientContactView> GetAll(int page = 1, int size = 10, string search = "");
    Task<ClientContactDto> GetById(int id);
    Task<ClientContactView> GetByClientId(int page = 1, int size = 10, int userId = 0);
    Task<ClientContactDto> Create(ClientContactCreateDto clientContactCreateDto);
    Task<ClientContactDto> Update(ClientContactCreateDto clientContactCreateDto);
    Task<bool> Delete(int id);
}
