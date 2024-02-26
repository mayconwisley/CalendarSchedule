using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IClientService
{
    Task<IEnumerable<ClientDto>> GetAll(int page, int size, string search);
    Task<ClientDto> GetById(int id);
    Task Create(ClientDto clientDto);
    Task Update(ClientDto clientDto);
    Task Delete(int id);
    Task<int> TotalClients(string search);
}
