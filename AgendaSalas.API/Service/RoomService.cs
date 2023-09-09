using ScheduleRooms.API.MappingDto;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _salaRepository;

    public RoomService(IRoomRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task Create(RoomDto roomDto)
    {
        await _salaRepository.Create(roomDto.ConverterDtoParaSala());
    }

    public async Task Delete(int id)
    {
        var salaEntity = await GetById(id);
        if (salaEntity is not null)
        {
            await _salaRepository.Delete(salaEntity.Id);
        }
    }

    public async Task<IEnumerable<RoomDto>> GetAll(int page, int size, string search)
    {
        var salaEntity = await _salaRepository.GetAll(page, size, search);
        return salaEntity.ConverterSalasParaDto();
    }

    public async Task<RoomDto> GetById(int id)
    {
        var salaEntity = await _salaRepository.GetById(id);
        return salaEntity.ConverterSalaParaDto();
    }

    public async Task<int> TotalSalas(string search)
    {
        var totalSala = await _salaRepository.TotalRooms(search);
        return totalSala;
    }

    public async Task Update(RoomDto roomDto)
    {
        await _salaRepository.Update(roomDto.ConverterDtoParaSala());
    }
}
