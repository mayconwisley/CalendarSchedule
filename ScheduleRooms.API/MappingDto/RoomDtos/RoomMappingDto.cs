using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.RoomDtos;

public static class RoomMappingDto
{
    public static IEnumerable<RoomDto> ConvertRoomsToDto(this IEnumerable<Room> rooms)
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
    public static IEnumerable<Room> ConvertDtoToRooms(this IEnumerable<RoomDto> roomsDto)
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
    public static RoomDto ConvertRoomToDto(this Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            Name = room.Name,
            Description = room.Description,
            Ramal = room.Ramal
        };
    }
    public static Room ConvertDtoToRoom(this RoomDto roomDto)
    {
        return new Room
        {
            Id = roomDto.Id,
            Name = roomDto.Name,
            Description = roomDto.Description,
            Ramal = roomDto.Ramal

        };
    }
}
