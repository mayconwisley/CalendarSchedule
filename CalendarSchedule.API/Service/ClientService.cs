using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ClientDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ClientService(IClientRepository _clientRepository) : IClientService
{
    public async Task<Result<ClientDto>> Create(ClientDto clientDto)
    {
        var clientCreate = await _clientRepository.Create(clientDto.ConvertDtoToClient());
        if (clientCreate.Id == 0)
            return Result.Failure<ClientDto>(Error.Internal("Erro ao criar cliente"));

        var dto = clientCreate.ConvertClientToDto();
        return Result.Success(dto);
    }

    public async Task<Result> Delete(int id)
    {
        var userEntity = await GetById(id);
        if (userEntity.IsFailure)
            return Result.Failure<ClientDto>(userEntity.Error);

        await _clientRepository.Delete(userEntity.Value.Id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<ClientDto>>> GetAll(int page, int size, string search)
    {
        var userEntity = await _clientRepository.GetAll(page, size, search);
        if (userEntity is null)
            return Result.Failure<IEnumerable<ClientDto>>(Error.NotFound("Nenhum cliente encontrado"));

        var dto = userEntity.ConvertClientsToDto();

        return Result.Success(dto);
    }

    public async Task<Result<ClientDto>> GetById(int id)
    {
        var userEntity = await _clientRepository.GetById(id);
        if (userEntity is null)
            return Result.Failure<ClientDto>(Error.NotFound("Nenhum cliente encontrado"));

        var dto = userEntity.ConvertClientToDto();

        return Result.Success(dto);
    }

    public async Task<Result<int>> TotalClients(string search)
    {
        var totalUser = await _clientRepository.TotalClients(search);

        if (totalUser <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum cliente encontrado"));

        return Result.Success(totalUser);
    }

    public async Task<Result<ClientDto>> Update(ClientDto clientDto)
    {
        var client = await _clientRepository.Update(clientDto.ConvertDtoToClient());
        if (client is null)
            return Result.Failure<ClientDto>(Error.NotFound("Nenhum cliente encontrado"));

        var dto = client.ConvertClientToDto();
        return Result.Success(dto);
    }
}
