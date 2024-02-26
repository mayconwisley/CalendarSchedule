using ScheduleRooms.API.MappingDto.ScheduleRoomDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ScheduleRoomService : IScheduleRoomService
{
    private readonly IScheduleRoomRepository _scheduleRepository;

    public ScheduleRoomService(IScheduleRoomRepository scheduleRepository)
    {
        _scheduleRepository = scheduleRepository;
    }

    public async Task Create(ScheduleRoomDto scheduleDto)
    {
        await _scheduleRepository.Create(scheduleDto.ConverterDtoParaAgenda());
    }

    public async Task Delete(int id)
    {
        var agendaEntity = await GetById(id);
        if (agendaEntity is not null)
        {
            await _scheduleRepository.Delete(agendaEntity.Id);
        }
    }

    public async Task<IEnumerable<ScheduleRoomDto>> GetAll(int page, int size, string search)
    {
        var agendaEntity = await _scheduleRepository.GetAll(page, size, search);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActive()
    {
        var agendaEntity = await _scheduleRepository.GetByAgendaActive();
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected)
    {
        var agendaEntity = await _scheduleRepository.GetByAgendaActiveSalaId(roomId, dateSalected);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<ScheduleRoomDto> GetById(int id)
    {
        var agendaEntity = await _scheduleRepository.GetById(id);
        return agendaEntity.ConverterAgendaParaDto();
    }

    public async Task<int> TotalAgendas(string search)
    {
        var totalAgenda = await _scheduleRepository.TotalSchedules(search);
        return totalAgenda;
    }

    public async Task Update(ScheduleRoomDto scheduleDto)
    {
        await _scheduleRepository.Update(scheduleDto.ConverterDtoParaAgenda());
    }
}
