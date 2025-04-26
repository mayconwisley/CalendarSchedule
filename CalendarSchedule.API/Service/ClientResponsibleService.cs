using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ClientResponsibleDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ClientResponsibleService(IClientResponsibleRepository _clientResponsibleRepository) : IClientResponsibleService
{
    public async Task<Result<ClientResponsibleDto>> Create(ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        var clientResponsible = await _clientResponsibleRepository.Create(clientResponsibleCreateDto.ConvertDtoToClientResponsibleCreate());
        if (clientResponsible.Id == 0)
            return Result.Failure<ClientResponsibleDto>(Error.BadRequest("Cliente responsavel não cadastrado"));

        var dto = clientResponsible.ConvertClientResponsibleToDto();
        return Result.Success(dto);
    }

    public async Task<Result> Delete(int id)
    {
        var clientResponsible = await GetById(id);
        if (clientResponsible.IsFailure)
            return Result.Failure(clientResponsible.Error);

        await _clientResponsibleRepository.Delete(clientResponsible.Value.Id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<ClientResponsibleDto>>> GetAll(int page, int size, string search)
    {
        var clientResponsibles = await _clientResponsibleRepository.GetAll(page, size, search);
        if (clientResponsibles is null)
            return Result.Failure<IEnumerable<ClientResponsibleDto>>(Error.NotFound("Cliente responsavel não encontrado"));
        var dto = clientResponsibles.ConvertClientResponsibleToDtos();
        return Result.Success(dto);
    }

    public async Task<Result<ClientResponsibleDto>> GetById(int id)
    {
        var clientResponsible = await _clientResponsibleRepository.GetById(id);
        if (clientResponsible is null)
            return Result.Failure<ClientResponsibleDto>(Error.NotFound("Cliente responsavel não encontrado"));

        var dto = clientResponsible.ConvertClientResponsibleToDto();
        return Result.Success(dto);
    }

    public async Task<Result<int>> TotalClientResponsible(string search)
    {
        var totalClientContact = await _clientResponsibleRepository.TotalClientResponsible(search);
        if (totalClientContact == 0)
            return Result.Failure<int>(Error.NotFound("Nenhum cliente responsavel encontrado"));

        return Result.Success(totalClientContact);
    }

    public async Task<Result<ClientResponsibleDto>> Update(ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        var clientResponsible = await _clientResponsibleRepository.Update(clientResponsibleCreateDto.ConvertDtoToClientResponsibleCreate());
        if (clientResponsible is null)
            return Result.Failure<ClientResponsibleDto>(Error.NotFound("Cliente responsavel não encontrado"));

        var dto = clientResponsible.ConvertClientResponsibleToDto();
        return Result.Success(dto);
    }
}
