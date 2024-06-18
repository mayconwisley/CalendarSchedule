using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ClientContactService : IClientContactService
{
    public Task<ClientContactDto> Create(ClientContactDto clientContactDto)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClientContactDto>> GetAll(int page, int size, string search)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClientContactDto>> GetByClientId(int page, int size, int clientId)
    {
        throw new NotImplementedException();
    }

    public Task<ClientContactDto> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<int> TotalClientContact(string search)
    {
        throw new NotImplementedException();
    }

    public Task<ClientContactDto> Update(ClientContactDto clientContactDto)
    {
        throw new NotImplementedException();
    }
}
