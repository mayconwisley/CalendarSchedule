using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.ClientResponsibleDtos;

public static class ClientResponsibleMappingDto
{
    public static IEnumerable<ClientResponsibleDto> ConvertClientResponsibleToDtos(this IEnumerable<ClientResponsible> clientResponsibles)
    {
        return clientResponsibles.Select(p => p.ConvertClientResponsibleToDto());
    }
    public static IEnumerable<ClientResponsible> ConvertDtoToClientResponsibles(this IEnumerable<ClientResponsibleDto> clientResponsibleDtos)
    {
        return clientResponsibleDtos.Select(p => p.ConvertDtoToClientResponsible());
    }
    public static ClientResponsibleDto ConvertClientResponsibleToDto(this ClientResponsible clientResponsible)
    {
        return new ClientResponsibleDto
        {
            Id = clientResponsible.Id,
            Name = clientResponsible.Name,
            Email = clientResponsible.Email,
            Description = clientResponsible.Description,
            Position = clientResponsible.Position,
            Active = clientResponsible.Active
        };
    }
    public static ClientResponsible ConvertDtoToClientResponsible(this ClientResponsibleDto clientResponsibleDto)
    {
        return new ClientResponsible
        {
            Id = clientResponsibleDto.Id,
            Name = clientResponsibleDto.Name,
            Email = clientResponsibleDto.Email,
            Description = clientResponsibleDto.Description,
            Position = clientResponsibleDto.Position,
            Active = clientResponsibleDto.Active
        };
    }
    public static ClientResponsibleCreateDto ConvertClientResponsibleCreateToDto(this ClientResponsible clientResponsible)
    {
        return new ClientResponsibleCreateDto
        {
            Name = clientResponsible.Name,
            Email = clientResponsible.Email,
            Description = clientResponsible.Description,
            Position = clientResponsible.Position,
            Active = clientResponsible.Active
        };
    }
    public static ClientResponsible ConvertDtoToClientResponsibleCreate(this ClientResponsibleCreateDto clientResponsibleCreateDto)
    {
        return new ClientResponsible
        {
            Name = clientResponsibleCreateDto.Name,
            Email = clientResponsibleCreateDto.Email,
            Description = clientResponsibleCreateDto.Description,
            Position = clientResponsibleCreateDto.Position,
            Active = clientResponsibleCreateDto.Active
        };
    }
}
