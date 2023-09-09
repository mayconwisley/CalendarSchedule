using ScheduleRooms.API.MappingDto;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ScheduleService : IScheduleService
{
    private readonly IScheduleRepository _agendaRepository;

    public ScheduleService(IScheduleRepository agendaRepository)
    {
        _agendaRepository = agendaRepository;
    }

    public async Task Create(ScheduleDto scheduleDto)
    {
        await _agendaRepository.Create(scheduleDto.ConverterDtoParaAgenda());
    }

    public async Task Delete(int id)
    {
        var agendaEntity = await GetById(id);
        if (agendaEntity is not null)
        {
            await _agendaRepository.Delete(agendaEntity.Id);
        }
    }

    public async Task<IEnumerable<ScheduleDto>> GetAll(int page, int size, string search)
    {
        var agendaEntity = await _agendaRepository.GetAll(page, size, search);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<ScheduleDto>> GetByAgendaActive()
    {
        var agendaEntity = await _agendaRepository.GetByAgendaActive();
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<ScheduleDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected)
    {
        var agendaEntity = await _agendaRepository.GetByAgendaActiveSalaId(roomId, dateSalected);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<ScheduleDto> GetById(int id)
    {
        var agendaEntity = await _agendaRepository.GetById(id);
        return agendaEntity.ConverterAgendaParaDto();
    }

    public async Task<int> TotalAgendas(string search)
    {
        var totalAgenda = await _agendaRepository.TotalSchedules(search);
        return totalAgenda;
    }

    public async Task Update(ScheduleDto scheduleDto)
    {
        await _agendaRepository.Update(scheduleDto.ConverterDtoParaAgenda());
    }
}
