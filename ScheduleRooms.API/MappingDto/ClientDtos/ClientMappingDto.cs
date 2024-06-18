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
                 
                    Telephone = client.Telephone,
                  
                    Description = client.Description,
                    City = client.City,
                   
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
                   
                    Description = client.Description,
                    City = client.City,
                
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
            Description = client.Description,
          
            City = client.City,
         
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
       
            Description = clientDto.Description,
            City = clientDto.City,
         
            Active = clientDto.Active,
            Prospection = clientDto.Prospection != null && clientDto.Prospection
        };
    }
}
