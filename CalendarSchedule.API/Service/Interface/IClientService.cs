using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IClientService
{
    Task<Result<PagedResult<ClientDto>>> GetAll(int page, int size, string search);
    Task<Result<ClientDto>> GetById(int id);
    Task<Result<ClientDto>> Create(ClientCreateDto clientCreateDto);
    Task<Result<ClientDto>> Update(ClientDto clientDto);
    Task<Result<ClientDto>> Delete(int id);
    Task<Result<int>> TotalClients(string search);
}
