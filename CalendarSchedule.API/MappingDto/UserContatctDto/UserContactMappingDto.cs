using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.UserContactContatctDto;

public static class UserContactContactMappingDto
{
    public static IEnumerable<UserContactDto> ConvertUserContactsToDto(this IEnumerable<UserContact> userContacts)
    {
        return (from userContact in userContacts
                select new UserContactDto
                {
                    Id = userContact.Id,
                    Number = userContact.Number,
                    Type = userContact.Type,
                    UserId = userContact.UserId,
                    UserDto = new()
                    {
                        Id = userContact.User.Id,
                        Active = userContact.User.Active,
                        Description = userContact.User.Description,
                        Manager = userContact.User.Manager,
                        Name = userContact.User.Name,
                        Username = userContact.User.Username,
                        Password = userContact.User.Password
                    }
                }).ToList();

    }
    public static IEnumerable<UserContact> ConvertDtoToUserContacts(this IEnumerable<UserContactDto> userContactDtos)
    {
        return (from userContact in userContactDtos
                select new UserContact
                {
                    Id = userContact.Id,
                    Number = userContact.Number,
                    Type = userContact.Type,
                    UserId = userContact.UserId,
                    User = new()
                    {
                        Id = userContact.UserDto.Id,
                        Active = userContact.UserDto.Active,
                        Description = userContact.UserDto.Description,
                        Manager = userContact.UserDto.Manager,
                        Name = userContact.UserDto.Name,
                        Username = userContact.UserDto.Username,
                        Password = userContact.UserDto.Password
                    }
                }).ToList();
    }
    public static UserContactDto ConvertUserContactToDto(this UserContact userContact)
    {
        return new UserContactDto
        {
            Id = userContact.Id,
            Number = userContact.Number,
            Type = userContact.Type,
            UserId = userContact.UserId,
            UserDto = new()
            {
                Id = userContact.User.Id,
                Active = userContact.User.Active,
                Description = userContact.User.Description,
                Manager = userContact.User.Manager,
                Name = userContact.User.Name,
                Username = userContact.User.Username,
                Password = userContact.User.Password
            }

        };
    }
    public static UserContact ConvertDtoToUserContact(this UserContactDto userContactDto)
    {
        return new UserContact
        {
            Id = userContactDto.Id,
            Number = userContactDto.Number,
            Type = userContactDto.Type,
            UserId = userContactDto.UserId,
            User = new()
            {
                Id = userContactDto.UserDto.Id,
                Active = userContactDto.UserDto.Active,
                Description = userContactDto.UserDto.Description,
                Manager = userContactDto.UserDto.Manager,
                Name = userContactDto.UserDto.Name,
                Username = userContactDto.UserDto.Username,
                Password = userContactDto.UserDto.Password
            }
        };
    }

    public static UserContactCreateDto ConvertUserContactCreateToDto(this UserContact userContact)
    {
        return new UserContactCreateDto
        {
            Id = userContact.Id,
            Number = userContact.Number,
            Type = userContact.Type,
            UserId = userContact.UserId,
        };
    }
    public static UserContact ConvertDtoToUserContactCreate(this UserContactCreateDto userContactCreateDto)
    {
        return new UserContact
        {
            Id = userContactCreateDto.Id,
            Number = userContactCreateDto.Number,
            Type = userContactCreateDto.Type,
            UserId = userContactCreateDto.UserId
        };
    }
}
