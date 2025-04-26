using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IClientService
{
    Task<Result<IEnumerable<ClientDto>>> GetAll(int page, int size, string search);
    Task<Result<ClientDto>> GetById(int id);
    Task<Result<ClientDto>> Create(ClientDto clientDto);
    Task<Result<ClientDto>> Update(ClientDto clientDto);
    Task<Result> Delete(int id);
    Task<Result<int>> TotalClients(string search);
}
