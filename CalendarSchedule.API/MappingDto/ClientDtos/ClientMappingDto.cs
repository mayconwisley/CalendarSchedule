using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.ClientDtos;

public static class ClientMappingDto
{
    public static IEnumerable<ClientDto> ConvertClientsToDto(this IEnumerable<Client> clients)
    {
        return (from client in clients
                select new ClientDto
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
                    Prospection = client.Prospection != null && client.Prospection

                }).ToList();

    }
    public static IEnumerable<Client> ConvertDtoToClients(this IEnumerable<ClientDto> clientDtos)
    {
        return (from client in clientDtos
                select new Client
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
                    Prospection = client.Prospection != null && client.Prospection

                }).ToList();
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
            Prospection = client.Prospection != null && client.Prospection

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
            Prospection = clientDto.Prospection != null && clientDto.Prospection
        };
    }
}
