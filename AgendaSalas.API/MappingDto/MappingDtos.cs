using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto;

public static class MappingDtos
{
    public static IEnumerable<RoomDto> ConverterSalasParaDto(this IEnumerable<Room> rooms)
    {
        return (from room in rooms
                select new RoomDto
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description,
                    Ramal = room.Ramal
                }).ToList();
    }

    public static IEnumerable<ScheduleDto> ConverterAgendasParaDto(this IEnumerable<Schedule> schedules)
    {
        return (from schedule in schedules
                select new ScheduleDto
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

    public static IEnumerable<Room> ConverterDtoParaSalas(this IEnumerable<RoomDto> roomsDto)
    {
        return (from room in roomsDto
                select new Room
                {
                    Id = room.Id,
                    Name = room.Name,
                    Description = room.Description,
                    Ramal = room.Ramal

                }).ToList();
    }

    public static IEnumerable<Schedule> ConverterAgendasParaDto(this IEnumerable<ScheduleDto> scheduleDto)
    {
        return (from schedule in scheduleDto
                select new Schedule
                {
                    Id = schedule.Id,
                    Description = schedule.Description,
                    DateStart = schedule.DateStart,
                    DateFinal = schedule.DateStart,
                    AllowCall = schedule.AllowCall,
                    AllowChat = schedule.AllowCall,
                    RoomId = schedule!.RoomId

                }).ToList();
    }

    public static RoomDto ConverterSalaParaDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            Ramal = room.Ramal
        };
    }
    public static ScheduleDto ConverterAgendaParaDto(this Schedule schedule)
    {
        return new ScheduleDto
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
    public static Room ConverterDtoParaSala(this RoomDto roomDto)
    {
        return new Room
        {
            Id = roomDto.Id,
            Name = roomDto.Name,
            Description = roomDto.Description,
            Ramal = roomDto.Ramal

        };
    }
    public static Schedule ConverterDtoParaAgenda(this ScheduleDto scheduleDto)
    {
        return new Schedule
        {
            Id = scheduleDto.Id,
            Description = scheduleDto.Description,
            DateStart = scheduleDto.DateStart,
            DateFinal = scheduleDto.DateStart,
            AllowChat = scheduleDto.AllowChat,
            AllowCall = scheduleDto.AllowCall,
            RoomId = scheduleDto!.RoomId
        };
    }

}
