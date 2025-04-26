using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ClientContactDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ClientContactService(IClientContactRepository _clientContactRepository) : IClientContactService
{
    public async Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto)
    {
        var clientContact = await _clientContactRepository.Create(clientContactCreateDto.ConvertDtoToClientContactCreate());
        if (clientContact.Id == 0)
            return Result.Failure<ClientContactDto>(Error.Internal("Erro ao criar contato"));

        var dto = clientContact.ConvertClientContactToDto();
        return Result.Success(dto);
    }

    public async Task<Result> Delete(int id)
    {
        var clientContact = await GetById(id);
        if (clientContact.IsFailure)
            return Result.Failure(clientContact.Error);

        await _clientContactRepository.Delete(clientContact.Value.Id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<ClientContactDto>>> GetAll(int page, int size, string search)
    {
        var clientContacts = await _clientContactRepository.GetAll(page, size, search);
        if (clientContacts is null)
            return Result.Failure<IEnumerable<ClientContactDto>>(Error.NotFound("Nenhum contato encontrado"));

        var dto = clientContacts!.ConvertClientContactsToDto();

        return Result.Success(dto);
    }

    public async Task<Result<IEnumerable<ClientContactDto>>> GetByClientId(int page, int size, int clientId)
    {
        var clientContacts = await _clientContactRepository.GetByClientId(page, size, clientId);
        if (clientContacts is null)
            return Result.Failure<IEnumerable<ClientContactDto>>(Error.NotFound("Nenhum contato encontrado"));

        var dto = clientContacts!.ConvertClientContactsToDto();

        return Result.Success(dto);
    }

    public async Task<Result<ClientContactDto>> GetById(int id)
    {
        var clientContact = await _clientContactRepository.GetById(id);
        if (clientContact is null)
            return Result.Failure<ClientContactDto>(Error.NotFound("Nenhum contato encontrado"));

        var dto = clientContact.ConvertClientContactToDto();
        return Result.Success(dto);
    }

    public async Task<Result<int>> TotalClientContact(string search)
    {
        var totalClientContact = await _clientContactRepository.TotalClientContact(search);
        if (totalClientContact <= 0)
            return Result.Failure<int>(Error.BadRequest("Erro ao totalizar contato"));

        return Result.Success(totalClientContact);
    }

    public async Task<Result<ClientContactDto>> Update(ClientContactCreateDto clientContactCreateDto)
    {
        var clientContact = await _clientContactRepository.Update(clientContactCreateDto.ConvertDtoToClientContactCreate());
        if (clientContact is null)
            return Result.Failure<ClientContactDto>(Error.Internal("Erro ao atualizar contato"));

        var dto = clientContact.ConvertClientContactToDto();
        return Result.Success(dto);
    }
}
