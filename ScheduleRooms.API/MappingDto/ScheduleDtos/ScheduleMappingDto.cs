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
                    Particular = schedule.Particular

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
                    Particular = schedule.Particular
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
            Particular = schedule.Particular
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
            Particular = scheduleDto.Particular
        };
    }
}
