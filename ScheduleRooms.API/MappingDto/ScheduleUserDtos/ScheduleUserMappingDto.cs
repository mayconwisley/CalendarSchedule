using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.ScheduleUserDtos;

public static class ScheduleUserMappingDto
{
    public static IEnumerable<ScheduleUserDto> ConvertSchedulesToDto(this IEnumerable<ScheduleUser> schedulesUser)
    {
        return (from scheduleUser in schedulesUser
                select new ScheduleUserDto
                {
                    Id = scheduleUser.Id,
                    Description = scheduleUser.Description,
                    DateStart = scheduleUser.DateStart,
                    DateFinal = scheduleUser.DateFinal,
                    MeetingType = scheduleUser.MeetingType,
                    StatusSchedule = scheduleUser.StatusSchedule,
                    ClientId = scheduleUser?.Client?.Id,
                    Client = scheduleUser?.Client?.Name,
                    City = scheduleUser?.Client.City,
                    User = scheduleUser?.User?.Name,
                    UserId = scheduleUser!.User!.Id,
                    ManagerId = scheduleUser.ManagerId,
                    Prospection = scheduleUser?.Client?.Prospection == null ? false : scheduleUser.Client.Prospection,
                    Particular = scheduleUser.Particular

                }).ToList();
    }
    public static IEnumerable<ScheduleUser> ConvertScheduleToDto(this IEnumerable<ScheduleUserDto> scheduleUserDto)
    {
        return (from scheduleUser in scheduleUserDto
                select new ScheduleUser
                {
                    Id = scheduleUser.Id,
                    Description = scheduleUser.Description,
                    DateStart = scheduleUser.DateStart,
                    DateFinal = scheduleUser.DateFinal,
                    MeetingType = scheduleUser.MeetingType,
                    StatusSchedule = scheduleUser.StatusSchedule,
                    ClientId = scheduleUser.ClientId,
                    UserId = scheduleUser.UserId,
                    ManagerId = scheduleUser.ManagerId,
                    Particular = scheduleUser.Particular
                }).ToList();
    }
    public static ScheduleUserDto ConvertScheduleToDto(this ScheduleUser scheduleUser)
    {
        return new ScheduleUserDto
        {
            Id = scheduleUser.Id,
            Description = scheduleUser.Description,
            DateStart = scheduleUser.DateStart,
            DateFinal = scheduleUser.DateFinal,
            MeetingType = scheduleUser.MeetingType,
            StatusSchedule = scheduleUser.StatusSchedule,
            ClientId = scheduleUser!.Client!.Id,
            Client = scheduleUser?.Client?.Name,
            City = scheduleUser!.Client.City,
            User = scheduleUser?.User?.Name,
            UserId = scheduleUser!.User!.Id,
            ManagerId = scheduleUser.ManagerId,
            Prospection = scheduleUser.Client.Prospection != null && scheduleUser.Client.Prospection,
            Particular = scheduleUser.Particular
        };
    }
    public static ScheduleUser ConvertDtoToSchedule(this ScheduleUserDto scheduleUserDto)
    {
        return new ScheduleUser
        {
            Id = scheduleUserDto.Id,
            Description = scheduleUserDto.Description,
            DateStart = scheduleUserDto.DateStart,
            DateFinal = scheduleUserDto.DateFinal,
            MeetingType = scheduleUserDto.MeetingType,
            StatusSchedule = scheduleUserDto.StatusSchedule,
            ClientId = scheduleUserDto.ClientId,
            UserId = scheduleUserDto.UserId,
            ManagerId = scheduleUserDto.ManagerId,
            Particular = scheduleUserDto.Particular
        };
    }
}
