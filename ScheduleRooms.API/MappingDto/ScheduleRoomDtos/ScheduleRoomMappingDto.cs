using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.ScheduleRoomDtos;

public static class ScheduleRoomMappingDto
{
    public static IEnumerable<ScheduleRoomDto> ConverterAgendasParaDto(this IEnumerable<ScheduleRoom> schedules)
    {
        return (from schedule in schedules
                select new ScheduleRoomDto
                {
                    Id = schedule.Id,
                    Description = schedule.Description,
                    DateStart = schedule.DateStart,
                    DateFinal = schedule.DateFinal,
                    AllowCall = schedule.AllowCall,
                    AllowChat = schedule.AllowChat,
                    Room = schedule?.Room?.Name,
                    RoomId = schedule!.Room!.Id

                }).ToList();
    }
    public static IEnumerable<ScheduleRoom> ConverterAgendasParaDto(this IEnumerable<ScheduleRoomDto> scheduleDto)
    {
        return (from schedule in scheduleDto
                select new ScheduleRoom
                {
                    Id = schedule.Id,
                    Description = schedule.Description,
                    DateStart = schedule.DateStart,
                    DateFinal = schedule.DateFinal,
                    AllowCall = schedule.AllowCall,
                    AllowChat = schedule.AllowChat,
                    RoomId = schedule!.RoomId

                }).ToList();
    }
    public static ScheduleRoomDto ConverterAgendaParaDto(this ScheduleRoom schedule)
    {
        return new ScheduleRoomDto
        {
            Id = schedule.Id,
            Description = schedule.Description,
            DateStart = schedule.DateStart,
            DateFinal = schedule.DateFinal,
            AllowChat = schedule.AllowChat,
            AllowCall = schedule.AllowCall,
            Room = schedule?.Room?.Name,
            RoomId = schedule!.Room!.Id
        };
    }
    public static ScheduleRoom ConverterDtoParaAgenda(this ScheduleRoomDto scheduleDto)
    {
        return new ScheduleRoom
        {
            Id = scheduleDto.Id,
            Description = scheduleDto.Description,
            DateStart = scheduleDto.DateStart,
            DateFinal = scheduleDto.DateFinal,
            AllowChat = scheduleDto.AllowChat,
            AllowCall = scheduleDto.AllowCall,
            RoomId = scheduleDto!.RoomId
        };
    }

}
