using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.ClientDtos;

public static class ClientMappingDto
{
    public static IEnumerable<ClientDto> ConvertClientsToDto(this IEnumerable<Client> clients)
    {
        return (from client in clients
                select new ClientDto
                {
                    Id = client.Id,
                    Name = client.Name,
                    Responsible = client.Responsible,
                    Telephone = client.Telephone,
                    Description = client.Description,
                    Active = client.Active
                }).ToList();

    }
    public static IEnumerable<Client> ConvertDtoToClients(this IEnumerable<ClientDto> clientDtos)
    {
        return (from client in clientDtos
                select new Client
                {
                    Id = client.Id,
                    Name = client.Name,
                    Responsible = client.Responsible,
                    Telephone = client.Telephone,
                    Description = client.Description,
                    Active = client.Active
                }).ToList();
    }
    public static ClientDto ConvertClientToDto(this Client client)
    {
        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Responsible = client.Responsible,
            Telephone = client.Telephone,
            Description = client.Description,
            Active = client.Active

        };
    }
    public static Client ConvertDtoToClient(this ClientDto clientDto)
    {
        return new Client
        {
            Id = clientDto.Id,
            Name = clientDto.Name,
            Responsible = clientDto.Responsible,
            Telephone = clientDto.Telephone,
            Description = clientDto.Description,
            Active = clientDto.Active
        };
    }
}
