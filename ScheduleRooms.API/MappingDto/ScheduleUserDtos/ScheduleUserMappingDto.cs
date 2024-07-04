using ScheduleRooms.API.MappingDto.ClientDtos;
using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.ScheduleUserDtos;

public static class ScheduleUserMappingDto
{
    public static IEnumerable<ScheduleUserDto> ConvertSchedulesUserDto(this IEnumerable<ScheduleUser> scheduleUsers)
    {
        return (from scheduleUser in scheduleUsers
                select new ScheduleUserDto
                {
                    ScheduleId = scheduleUser.ScheduleId,
                    ScheduleDto = new ScheduleDto
                    {
                        Id = scheduleUser.Schedule.Id,
                        Description = scheduleUser.Schedule.Description,
                        DateStart = scheduleUser.Schedule.DateStart,
                        DateFinal = scheduleUser.Schedule.DateFinal,
                        MeetingType = scheduleUser.Schedule.MeetingType,
                        StatusSchedule = scheduleUser.Schedule.StatusSchedule,
                        Particular = scheduleUser.Schedule.Particular,
                        ClientId = scheduleUser.Schedule.ClientId,
                        ClientDto = new ClientDto
                        {
                            Id = scheduleUser.Schedule.Client.Id,
                            Name = scheduleUser.Schedule.Client.Name,
                            Description = scheduleUser.Schedule.Client.Description,
                            City = scheduleUser.Schedule.Client.City,
                            Garden = scheduleUser.Schedule.Client.Garden,
                            Road = scheduleUser.Schedule.Client.Road,
                            State = scheduleUser.Schedule.Client.State,
                            Number = scheduleUser.Schedule.Client.Number,
                            Telephone = scheduleUser.Schedule.Client.Telephone,
                            Prospection = scheduleUser.Schedule.Client.Prospection,
                            Active = scheduleUser.Schedule.Client.Active

                        }
                    },
                    UserId = scheduleUser.UserId,
                    UserDto = new UserDto
                    {
                        Id = scheduleUser.User.Id,
                        Description = scheduleUser.User.Description,
                        Name = scheduleUser.User.Name,
                        Username = scheduleUser.User.Username,
                        Password = scheduleUser.User.Password,
                        Manager = scheduleUser.User.Manager,
                        Active = scheduleUser.User.Active
                    }
                }

            ).ToList();
    }
    public static IEnumerable<ScheduleUser> ConvertDtoScheduleUsers(this IEnumerable<ScheduleUserDto> scheduleUserDtos)
    {
        return (from scheduleUserDto in scheduleUserDtos
                select new ScheduleUser
                {
                    ScheduleId = scheduleUserDto.ScheduleId,
                    Schedule = new Schedule
                    {
                        Id = scheduleUserDto.ScheduleDto.Id,
                        Description = scheduleUserDto.ScheduleDto.Description,
                        DateStart = scheduleUserDto.ScheduleDto.DateStart,
                        DateFinal = scheduleUserDto.ScheduleDto.DateFinal,
                        MeetingType = scheduleUserDto.ScheduleDto.MeetingType,
                        StatusSchedule = scheduleUserDto.ScheduleDto.StatusSchedule,
                        Particular = scheduleUserDto.ScheduleDto.Particular,
                        ClientId = scheduleUserDto.ScheduleDto.ClientId,
                        Client = new Client
                        {
                            Id = scheduleUserDto.ScheduleDto.ClientDto.Id,
                            Name = scheduleUserDto.ScheduleDto.ClientDto.Name,
                            Description = scheduleUserDto.ScheduleDto.ClientDto.Description,
                            City = scheduleUserDto.ScheduleDto.ClientDto.City,
                            Garden = scheduleUserDto.ScheduleDto.ClientDto.Garden,
                            Road = scheduleUserDto.ScheduleDto.ClientDto.Road,
                            State = scheduleUserDto.ScheduleDto.ClientDto.State,
                            Number = scheduleUserDto.ScheduleDto.ClientDto.Number,
                            Telephone = scheduleUserDto.ScheduleDto.ClientDto.Telephone,
                            Prospection = scheduleUserDto.ScheduleDto.ClientDto.Prospection,
                            Active = scheduleUserDto.ScheduleDto.ClientDto.Active

                        }
                    },
                    UserId = scheduleUserDto.UserId,
                    User = new User
                    {
                        Id = scheduleUserDto.UserDto.Id,
                        Description = scheduleUserDto.UserDto.Description,
                        Name = scheduleUserDto.UserDto.Name,
                        Username = scheduleUserDto.UserDto.Username,
                        Password = scheduleUserDto.UserDto.Password,
                        Manager = scheduleUserDto.UserDto.Manager,
                        Active = scheduleUserDto.UserDto.Active
                    }
                }

            ).ToList();
    }


    public static ScheduleUser ConvertDtoScheduleUser(this ScheduleUserDto scheduleUserDto)
    {
        return new ScheduleUser
        {
            ScheduleId = scheduleUserDto.ScheduleId,
            Schedule = new Schedule
            {
                Id = scheduleUserDto.ScheduleDto.Id,
                Description = scheduleUserDto.ScheduleDto.Description,
                DateStart = scheduleUserDto.ScheduleDto.DateStart,
                DateFinal = scheduleUserDto.ScheduleDto.DateFinal,
                MeetingType = scheduleUserDto.ScheduleDto.MeetingType,
                StatusSchedule = scheduleUserDto.ScheduleDto.StatusSchedule,
                Particular = scheduleUserDto.ScheduleDto.Particular,
                ClientId = scheduleUserDto.ScheduleDto.ClientId,
                Client = new Client
                {
                    Id = scheduleUserDto.ScheduleDto.ClientDto.Id,
                    Name = scheduleUserDto.ScheduleDto.ClientDto.Name,
                    Description = scheduleUserDto.ScheduleDto.ClientDto.Description,
                    City = scheduleUserDto.ScheduleDto.ClientDto.City,
                    Garden = scheduleUserDto.ScheduleDto.ClientDto.Garden,
                    Road = scheduleUserDto.ScheduleDto.ClientDto.Road,
                    State = scheduleUserDto.ScheduleDto.ClientDto.State,
                    Number = scheduleUserDto.ScheduleDto.ClientDto.Number,
                    Telephone = scheduleUserDto.ScheduleDto.ClientDto.Telephone,
                    Prospection = scheduleUserDto.ScheduleDto.ClientDto.Prospection,
                    Active = scheduleUserDto.ScheduleDto.ClientDto.Active

                }
            },
            UserId = scheduleUserDto.UserId,
            User = new User
            {
                Id = scheduleUserDto.UserDto.Id,
                Description = scheduleUserDto.UserDto.Description,
                Name = scheduleUserDto.UserDto.Name,
                Username = scheduleUserDto.UserDto.Username,
                Password = scheduleUserDto.UserDto.Password,
                Manager = scheduleUserDto.UserDto.Manager,
                Active = scheduleUserDto.UserDto.Active
            }
        };
    }

    public static ScheduleUserDto ConvertScheduleUserDto(this ScheduleUser scheduleUser)
    {
        return new ScheduleUserDto
        {
            ScheduleId = scheduleUser.ScheduleId,
            ScheduleDto = new ScheduleDto
            {
                Id = scheduleUser.Schedule.Id,
                Description = scheduleUser.Schedule.Description,
                DateStart = scheduleUser.Schedule.DateStart,
                DateFinal = scheduleUser.Schedule.DateFinal,
                MeetingType = scheduleUser.Schedule.MeetingType,
                StatusSchedule = scheduleUser.Schedule.StatusSchedule,
                Particular = scheduleUser.Schedule.Particular,
                ClientId = scheduleUser.Schedule.ClientId,
                ClientDto = new ClientDto
                {
                    Id = scheduleUser.Schedule.Client.Id,
                    Name = scheduleUser.Schedule.Client.Name,
                    Description = scheduleUser.Schedule.Client.Description,
                    City = scheduleUser.Schedule.Client.City,
                    Garden = scheduleUser.Schedule.Client.Garden,
                    Road = scheduleUser.Schedule.Client.Road,
                    State = scheduleUser.Schedule.Client.State,
                    Number = scheduleUser.Schedule.Client.Number,
                    Telephone = scheduleUser.Schedule.Client.Telephone,
                    Prospection = scheduleUser.Schedule.Client.Prospection,
                    Active = scheduleUser.Schedule.Client.Active

                }
            },
            UserId = scheduleUser.UserId,
            UserDto = new UserDto
            {
                Id = scheduleUser.User.Id,
                Description = scheduleUser.User.Description,
                Name = scheduleUser.User.Name,
                Username = scheduleUser.User.Username,
                Password = scheduleUser.User.Password,
                Manager = scheduleUser.User.Manager,
                Active = scheduleUser.User.Active
            }
        };
    }

    public static ScheduleUserCreateDto ConvertScheduleUserCreateDto(this ScheduleUser scheduleUser)
    {
        return new ScheduleUserCreateDto
        {
            ScheduleId = scheduleUser.ScheduleId,
            UserId = scheduleUser.UserId
        };
    }

    public static ScheduleUser ConvertDtoScheduleUserCreate(this ScheduleUserCreateDto scheduleUserCreateDto)
    {
        return new ScheduleUser
        {
            ScheduleId = scheduleUserCreateDto.ScheduleId,
            UserId = scheduleUserCreateDto.UserId
        };
    }
}
