using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ClientDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ClientService(IClientRepository _clientRepository) : IClientService
{
    public async Task<Result<ClientDto>> Create(ClientCreateDto clientCreateDto)
    {
        var clientCreate = await _clientRepository.Create(clientCreateDto.ConvertDtoCreateToClient());
        if (clientCreate.Id == 0)
            return Result.Failure<ClientDto>(Error.Internal("Erro ao criar cliente"));

        var dto = clientCreate.ConvertClientToDto();
        return Result.Success(dto);
    }

    public async Task<Result<ClientDto>> Delete(int id)
    {
        var user = await GetById(id);
        if (user.IsFailure)
            return Result.Failure<ClientDto>(user.Error);

        var clientDto = await _clientRepository.Delete(user.Value.Id);
        if (clientDto is null)
            return Result.Failure<ClientDto>(Error.NotFound("Nenhum cliente encontrado"));

        var dto = clientDto.ConvertClientToDto();
        return Result.Success(dto);
    }

    public async Task<Result<PagedResult<ClientDto>>> GetAll(int page, int size, string search)
    {
        var clientEntity = await _clientRepository.GetAll(page, size, search);
        if (clientEntity is null)
            return Result.Failure<PagedResult<ClientDto>>(Error.NotFound("Nenhum cliente encontrado"));

        var totalClient = await _clientRepository.TotalClients(search);

        if (totalClient <= 0)
            return Result.Failure<PagedResult<ClientDto>>(Error.NotFound("Nenhum cliente encontrado"));

        decimal totalData = totalClient;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
        if (size == 1)
            totalPage = totalData;

        var dto = clientEntity.ConvertClientsToDto();

        var clientDto = new PagedResult<ClientDto>(dto, page, size, totalData, totalPage);

        return Result.Success(clientDto);
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
