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
                    Schedule = new ScheduleDto
                    {
                        Id = scheduleUser.Schedule.Id,
                        Description = scheduleUser.Schedule.Description,
                        DateStart = scheduleUser.Schedule.DateStart,
                        DateFinal = scheduleUser.Schedule.DateFinal,
                        MeetingType = scheduleUser.Schedule.MeetingType,
                        StatusSchedule = scheduleUser.Schedule.StatusSchedule,
                        Particular = scheduleUser.Schedule.Particular,
                        ClientId = scheduleUser.Schedule.ClientId,
                    },
                    UserId = scheduleUser.UserId,
                    User = new UserDto
                    {
                        Id = scheduleUser.User.Id,
                        Description = scheduleUser.User.Description,
                        Name = scheduleUser.User.Name,
                        Username = scheduleUser.User.Username,
                        Password = scheduleUser.User.Password,
                        Cellphone = scheduleUser.User.Cellphone,
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
                        Id = scheduleUserDto.Schedule.Id,
                        Description = scheduleUserDto.Schedule.Description,
                        DateStart = scheduleUserDto.Schedule.DateStart,
                        DateFinal = scheduleUserDto.Schedule.DateFinal,
                        MeetingType = scheduleUserDto.Schedule.MeetingType,
                        StatusSchedule = scheduleUserDto.Schedule.StatusSchedule,
                        Particular = scheduleUserDto.Schedule.Particular,
                        ClientId = scheduleUserDto.Schedule.ClientId,
                    },
                    UserId = scheduleUserDto.UserId,
                    User = new User
                    {
                        Id = scheduleUserDto.User.Id,
                        Description = scheduleUserDto.User.Description,
                        Name = scheduleUserDto.User.Name,
                        Username = scheduleUserDto.User.Username,
                        Password = scheduleUserDto.User.Password,
                        Cellphone = scheduleUserDto.User.Cellphone,
                        Manager = scheduleUserDto.User.Manager,
                        Active = scheduleUserDto.User.Active
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
                Id = scheduleUserDto.Schedule.Id,
                Description = scheduleUserDto.Schedule.Description,
                DateStart = scheduleUserDto.Schedule.DateStart,
                DateFinal = scheduleUserDto.Schedule.DateFinal,
                MeetingType = scheduleUserDto.Schedule.MeetingType,
                StatusSchedule = scheduleUserDto.Schedule.StatusSchedule,
                Particular = scheduleUserDto.Schedule.Particular,
                ClientId = scheduleUserDto.Schedule.ClientId,
            },
            UserId = scheduleUserDto.UserId,
            User = new User
            {
                Id = scheduleUserDto.User.Id,
                Description = scheduleUserDto.User.Description,
                Name = scheduleUserDto.User.Name,
                Username = scheduleUserDto.User.Username,
                Password = scheduleUserDto.User.Password,
                Cellphone = scheduleUserDto.User.Cellphone,
                Manager = scheduleUserDto.User.Manager,
                Active = scheduleUserDto.User.Active
            }
        };

    }

    public static ScheduleUserDto ConvertScheduleUserDto(this ScheduleUser scheduleUser)
    {
        return new ScheduleUserDto
        {
            ScheduleId = scheduleUser.ScheduleId,
            Schedule = new ScheduleDto
            {
                Id = scheduleUser.Schedule.Id,
                Description = scheduleUser.Schedule.Description,
                DateStart = scheduleUser.Schedule.DateStart,
                DateFinal = scheduleUser.Schedule.DateFinal,
                MeetingType = scheduleUser.Schedule.MeetingType,
                StatusSchedule = scheduleUser.Schedule.StatusSchedule,
                Particular = scheduleUser.Schedule.Particular,
                ClientId = scheduleUser.Schedule.ClientId,
            },
            UserId = scheduleUser.UserId,
            User = new UserDto
            {
                Id = scheduleUser.User.Id,
                Description = scheduleUser.User.Description,
                Name = scheduleUser.User.Name,
                Username = scheduleUser.User.Username,
                Password = scheduleUser.User.Password,
                Cellphone = scheduleUser.User.Cellphone,
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
