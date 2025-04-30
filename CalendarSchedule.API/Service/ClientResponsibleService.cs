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

    public async Task<Result<ClientResponsibleDto>> Delete(int id)
    {
        var deletedReponsible = await _clientResponsibleRepository.Delete(id);
        if (deletedReponsible is null)
            return Result.Failure<ClientResponsibleDto>(Error.NotFound("Cliente responsavel não encontrado"));

        var deletedClientResponsible = deletedReponsible.ConvertClientResponsibleToDto();

        return Result.Success(deletedClientResponsible);
    }

    public async Task<Result<PagedResult<ClientResponsibleDto>>> GetAll(int page, int size, string search)
    {
        var clientResponsibles = await _clientResponsibleRepository.GetAll(page, size, search);
        if (clientResponsibles is null)
            return Result.Failure<PagedResult<ClientResponsibleDto>>(Error.NotFound("Cliente responsavel não encontrado"));

        var totalClientResponsible = await _clientResponsibleRepository.TotalClientResponsible(search);
        if (totalClientResponsible <= 0)
            return Result.Failure<PagedResult<ClientResponsibleDto>>(Error.NotFound("Nenhum cliente responsavel encontrado"));
        decimal totalData = totalClientResponsible;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
        if (size == 1)
            totalPage = totalData;

        var dto = clientResponsibles.ConvertClientResponsibleToDtos();
        var clientResponsibleDto = new PagedResult<ClientResponsibleDto>(dto, totalData, page, totalPage, size);

        return Result.Success(clientResponsibleDto);
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

    public async Task<Result<ClientResponsibleDto>> Update(ClientResponsibleDto clientResponsibleDto)
    {
        var clientResponsible = await _clientResponsibleRepository.Update(clientResponsibleDto.ConvertDtoToClientResponsible());
        if (clientResponsible is null)
            return Result.Failure<ClientResponsibleDto>(Error.NotFound("Cliente responsavel não encontrado"));

        var dto = clientResponsible.ConvertClientResponsibleToDto();
        return Result.Success(dto);
    }
}
