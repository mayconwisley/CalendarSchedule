using ScheduleRooms.API.MappingDto.ClientDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public async Task Create(ClientDto clientDto)
    {
        await _clientRepository.Create(clientDto.ConvertDtoToClient());
    }

    public async Task Delete(int id)
    {
        var userEntity = await GetById(id);
        if (userEntity is not null)
        {
            await _clientRepository.Delete(userEntity.Id);
        }
    }

    public async Task<IEnumerable<ClientDto>> GetAll(int page, int size, string search)
    {
        var userEntity = await _clientRepository.GetAll(page, size, search);
        return userEntity.ConvertClientsToDto();
    }

    public async Task<ClientDto> GetById(int id)
    {
        var userEntity = await _clientRepository.GetById(id);
        return userEntity.ConvertClientToDto();
    }

    public async Task<int> TotalClients(string search)
    {
        var totalUser = await _clientRepository.TotalClients(search);
        return totalUser;
    }

    public async Task Update(ClientDto clientDto)
    {
        await _clientRepository.Update(clientDto.ConvertDtoToClient());
    }

}
