using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.UserDtos;

public static class UserMappingDto
{
    public static IEnumerable<UserDto> ConvertUsersToDto(this IEnumerable<User> users)
    {
        return (from user in users
                select new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Cellphone = user.Cellphone,
                    Description = user.Description,
                    Active = user.Active
                }).ToList();

    }
    public static IEnumerable<User> ConvertDtoToUsers(this IEnumerable<UserDto> userDtos)
    {
        return (from user in userDtos
                select new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    Cellphone = user.Cellphone,
                    Description = user.Description,
                    Active = user.Active
                }).ToList();
    }
    public static UserDto ConvertUserToDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Cellphone = user.Cellphone,
            Description = user.Description,
            Active = user.Active

        };
    }
    public static User ConvertDtoToUser(this UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Cellphone = userDto.Cellphone,
            Description = userDto.Description,
            Active = userDto.Active
        };
    }
}
