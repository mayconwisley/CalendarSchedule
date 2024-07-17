using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.ClientContactDtos;

public static class ClientContactMappingDto
{
    public static IEnumerable<ClientContactDto> ConvertClientContactsToDto(this IEnumerable<ClientContact> clientContacts)
    {
        return (from clientContact in clientContacts
                select new ClientContactDto
                {
                    Id = clientContact.Id,
                    Number = clientContact.Number,
                    Type = clientContact.Type,
                    ClientId = clientContact.ClientId,
                    ClientDto = new()
                    {
                        Id = clientContact.Client.Id,
                        Name = clientContact.Client.Name,
                        Telephone = clientContact.Client.Telephone,
                        State = clientContact.Client.State,
                        City = clientContact.Client.City,
                        Road = clientContact.Client.Road,
                        Number = clientContact.Client.Number,
                        Garden = clientContact.Client.Garden,
                        Description = clientContact.Client.Description,
                        Active = clientContact.Client.Active,
                        Prospection = clientContact.Client.Prospection
                    },
                    ClientResponsibleId = clientContact.ClientResponsibleId,
                    ClientResponsibleDto = new()
                    {
                        Id = clientContact.ClientResponsible.Id,
                        Name = clientContact.ClientResponsible.Name,
                        Email = clientContact.ClientResponsible.Email,
                        Description = clientContact.ClientResponsible.Description,
                        Position = clientContact.ClientResponsible.Position,
                        Active = clientContact.ClientResponsible.Active,
                    }

                }).ToList();

    }
    public static IEnumerable<ClientContact> ConvertDtoToClientContacts(this IEnumerable<ClientContactDto> clientContactDtos)
    {
        return (from clientContact in clientContactDtos
                select new ClientContact
                {
                    Id = clientContact.Id,
                    Number = clientContact.Number,
                    Type = clientContact.Type,
                    ClientId = clientContact.ClientId,
                    Client = new()
                    {
                        Id = clientContact.ClientDto.Id,
                        Name = clientContact.ClientDto.Name,
                        Telephone = clientContact.ClientDto.Telephone,
                        State = clientContact.ClientDto.State,
                        City = clientContact.ClientDto.City,
                        Road = clientContact.ClientDto.Road,
                        Number = clientContact.ClientDto.Number,
                        Garden = clientContact.ClientDto.Garden,
                        Description = clientContact.ClientDto.Description,
                        Active = clientContact.ClientDto.Active,
                        Prospection = clientContact.ClientDto.Prospection
                    },
                    ClientResponsibleId = clientContact.ClientResponsibleId,
                    ClientResponsible = new()
                    {
                        Id = clientContact.ClientResponsibleDto.Id,
                        Name = clientContact.ClientResponsibleDto.Name,
                        Email = clientContact.ClientResponsibleDto.Email,
                        Description = clientContact.ClientResponsibleDto.Description,
                        Position = clientContact.ClientResponsibleDto.Position,
                        Active = clientContact.ClientResponsibleDto.Active,
                    }
                }).ToList();
    }
    public static ClientContactDto ConvertClientContactToDto(this ClientContact clientContact)
    {
        return new ClientContactDto
        {
            Id = clientContact.Id,
            Number = clientContact.Number,
            Type = clientContact.Type,
            ClientId = clientContact.ClientId,
            ClientDto = new()
            {
                Id = clientContact.Client.Id,
                Name = clientContact.Client.Name,
                Telephone = clientContact.Client.Telephone,
                State = clientContact.Client.State,
                City = clientContact.Client.City,
                Road = clientContact.Client.Road,
                Number = clientContact.Client.Number,
                Garden = clientContact.Client.Garden,
                Description = clientContact.Client.Description,
                Active = clientContact.Client.Active,
                Prospection = clientContact.Client.Prospection
            },
            ClientResponsibleId = clientContact.ClientResponsibleId,
            ClientResponsibleDto = new()
            {
                Id = clientContact.ClientResponsible.Id,
                Name = clientContact.ClientResponsible.Name,
                Email = clientContact.ClientResponsible.Email,
                Description = clientContact.ClientResponsible.Description,
                Position = clientContact.ClientResponsible.Position,
                Active = clientContact.ClientResponsible.Active,
            }

        };
    }
    public static ClientContact ConvertDtoToClientContact(this ClientContactDto clientContactDto)
    {
        return new ClientContact
        {
            Id = clientContactDto.Id,
            Number = clientContactDto.Number,
            Type = clientContactDto.Type,
            ClientId = clientContactDto.ClientId,
            Client = new()
            {
                Id = clientContactDto.ClientDto.Id,
                Name = clientContactDto.ClientDto.Name,
                Telephone = clientContactDto.ClientDto.Telephone,
                State = clientContactDto.ClientDto.State,
                City = clientContactDto.ClientDto.City,
                Road = clientContactDto.ClientDto.Road,
                Number = clientContactDto.ClientDto.Number,
                Garden = clientContactDto.ClientDto.Garden,
                Description = clientContactDto.ClientDto.Description,
                Active = clientContactDto.ClientDto.Active,
                Prospection = clientContactDto.ClientDto.Prospection
            },
            ClientResponsibleId = clientContactDto.ClientResponsibleId,
            ClientResponsible = new()
            {
                Id = clientContactDto.ClientResponsibleDto.Id,
                Name = clientContactDto.ClientResponsibleDto.Name,
                Email = clientContactDto.ClientResponsibleDto.Email,
                Description = clientContactDto.ClientResponsibleDto.Description,
                Position = clientContactDto.ClientResponsibleDto.Position,
                Active = clientContactDto.ClientResponsibleDto.Active,
            }
        };
    }

    public static ClientContactCreateDto ConvertClientContactCreateToDto(this ClientContact clientContact)
    {
        return new ClientContactCreateDto
        {
            Id = clientContact.Id,
            Number = clientContact.Number,
            Type = clientContact.Type,
            ClientId = clientContact.ClientId,
            ClientResponsibleId = clientContact.ClientResponsibleId,

        };
    }
    public static ClientContact ConvertDtoToClientContactCreate(this ClientContactCreateDto clientContactCreateDto)
    {
        return new ClientContact
        {
            Id = clientContactCreateDto.Id,
            Number = clientContactCreateDto.Number,
            Type = clientContactCreateDto.Type,
            ClientId = clientContactCreateDto.ClientId,
            ClientResponsibleId = clientContactCreateDto.ClientResponsibleId,
        };
    }
}
