using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.ClientDtos;

public static class ClientMappingDto
{
    public static IEnumerable<ClientDto> ConvertClientsToDto(this IEnumerable<Client> clients)
    {
        return clients.Select(s => s.ConvertClientToDto());
    }
    public static IEnumerable<Client> ConvertDtoToClients(this IEnumerable<ClientDto> clientDtos)
    {
        return clientDtos.Select(s => s.ConvertDtoToClient());
    }
    public static ClientDto ConvertClientToDto(this Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Telephone = client.Telephone,
            State = client.State,
            City = client.City,
            Road = client.Road,
            Number = client.Number,
            Garden = client.Garden,
            Description = client.Description,
            Active = client.Active,
            Prospection = client.Prospection
        };
    }
    public static Client ConvertDtoToClient(this ClientDto clientDto)
    {
        return new Client
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            Telephone = clientDto.Telephone,
            State = clientDto.State,
            City = clientDto.City,
            Road = clientDto.Road,
            Number = clientDto.Number,
            Garden = clientDto.Garden,
            Description = clientDto.Description,
            Active = clientDto.Active,
            Prospection = clientDto.Prospection
        };
    }

    public static Client ConvertDtoCreateToClient(this ClientCreateDto clientCreateDto)
    {
        return new Client
        {
            Name = clientCreateDto.Name,
            Telephone = clientCreateDto.Telephone,
            State = clientCreateDto.State,
            City = clientCreateDto.City,
            Road = clientCreateDto.Road,
            Number = clientCreateDto.Number,
            Garden = clientCreateDto.Garden,
            Description = clientCreateDto.Description,
            Active = clientCreateDto.Active,
            Prospection = clientCreateDto.Prospection
        };
    }
}
