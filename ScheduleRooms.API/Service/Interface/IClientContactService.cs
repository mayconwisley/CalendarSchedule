using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface;

public interface IClientContactService
{
    Task<IEnumerable<ClientContactDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<ClientContactDto>> GetByClientId(int page, int size, int clientId);
    Task<ClientContactDto> GetById(int id);
    Task<ClientContactDto> Create(ClientContactDto clientContactDto);
    Task<ClientContactDto> Update(ClientContactDto clientContactDto);
    Task Delete(int id);
    Task<int> TotalClientContact(string search);
}
