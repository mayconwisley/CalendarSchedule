using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IClientContactService
{
    Task<Result<IEnumerable<ClientContactDto>>> GetAll(int page, int size, string search);
    Task<Result<IEnumerable<ClientContactDto>>> GetByClientId(int page, int size, int clientId);
    Task<Result<ClientContactDto>> GetById(int id);
    Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto);
    Task<Result<ClientContactDto>> Update(ClientContactCreateDto clientContactCreateDto);
    Task<Result> Delete(int id);
    Task<Result<int>> TotalClientContact(string search);
}
