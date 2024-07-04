using ScheduleRooms.API.MappingDto.ClientDtos;
using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.ScheduleDtos;

public static class ScheduleMappingDto
{
    public static IEnumerable<ScheduleDto> ConvertSchedulesToDto(this IEnumerable<Schedule> schedules)
    {
        return (from schedule in schedules
                select new ScheduleDto
                {
                    Id = schedule.Id,
                    Description = schedule.Description,
                    DateStart = schedule.DateStart,
                    DateFinal = schedule.DateFinal,
                    MeetingType = schedule.MeetingType,
                    StatusSchedule = schedule.StatusSchedule,
                    ClientId = schedule?.Client?.Id,
                    ClientDto = schedule.Client.ConvertClientToDto(),
                    Particular = schedule.Particular,
                }).ToList();
    }
    public static IEnumerable<Schedule> ConvertScheduleToDto(this IEnumerable<ScheduleDto> scheduleDtos)
    {
        return (from schedule in scheduleDtos
                select new Schedule
                {
                    Id = schedule.Id,
                    Description = schedule.Description,
                    DateStart = schedule.DateStart,
                    DateFinal = schedule.DateFinal,
                    MeetingType = schedule.MeetingType,
                    StatusSchedule = schedule.StatusSchedule,
                    ClientId = schedule.ClientId,
                    Client = schedule.ClientDto.ConvertDtoToClient(),
                    Particular = schedule.Particular,
                }).ToList();
    }
    public static ScheduleDto ConvertScheduleToDto(this Schedule schedule)
    {
        return new ScheduleDto
        {
            Id = schedule.Id,
            Description = schedule.Description,
            DateStart = schedule.DateStart,
            DateFinal = schedule.DateFinal,
            MeetingType = schedule.MeetingType,
            StatusSchedule = schedule.StatusSchedule,
            ClientId = schedule?.Client?.Id,
            ClientDto = schedule.Client.ConvertClientToDto(),
            Particular = schedule.Particular,
        };
    }
    public static Schedule ConvertDtoToSchedule(this ScheduleDto scheduleDto)
    {
        return new Schedule
        {
            Id = scheduleDto.Id,
            Description = scheduleDto.Description,
            DateStart = scheduleDto.DateStart,
            DateFinal = scheduleDto.DateFinal,
            MeetingType = scheduleDto.MeetingType,
            StatusSchedule = scheduleDto.StatusSchedule,
            ClientId = scheduleDto.ClientId,
            Client = scheduleDto.ClientDto.ConvertDtoToClient(),
            Particular = scheduleDto.Particular,
        };
    }

    public static ScheduleCreateDto ConvertScheduleCreateToDto(this Schedule schedule)
    {
        return new ScheduleCreateDto
        {
            Id = schedule.Id,
            Description = schedule.Description,
            DateStart = schedule.DateStart,
            DateFinal = schedule.DateFinal,
            MeetingType = schedule.MeetingType,
            StatusSchedule = schedule.StatusSchedule,
            ClientId = schedule?.Client?.Id,
            Particular = schedule.Particular,
        };
    }
    public static Schedule ConvertDtoToScheduleCreate(this ScheduleCreateDto scheduleCreateDto)
    {
        return new Schedule
        {
            Id = scheduleCreateDto.Id,
            Description = scheduleCreateDto.Description,
            DateStart = scheduleCreateDto.DateStart,
            DateFinal = scheduleCreateDto.DateFinal,
            MeetingType = scheduleCreateDto.MeetingType,
            StatusSchedule = scheduleCreateDto.StatusSchedule,
            ClientId = scheduleCreateDto.ClientId,
            Particular = scheduleCreateDto.Particular,
        };
    }
}
