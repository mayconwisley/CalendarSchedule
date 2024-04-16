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
                    Email = client.Email,
                    Description = client.Description,
                    City = client.City,
                    Position = client.Position,
                    Active = client.Active,
                    Prospection = client.Prospection

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
                    Email = client.Email,
                    Description = client.Description,
                    City = client.City,
                    Position = client.Position,
                    Active = client.Active,
                    Prospection = client.Prospection

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
            Email = client.Email,
            City = client.City,
            Position = client.Position,
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
            Responsible = clientDto.Responsible,
            Telephone = clientDto.Telephone,
            Email = clientDto.Email,
            Description = clientDto.Description,
            City = clientDto.City,
            Position = clientDto.Position,
            Active = clientDto.Active,
            Prospection = clientDto.Prospection
        };
    }
}
