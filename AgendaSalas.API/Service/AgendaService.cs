using AgendaSalas.API.MappingDto;
using AgendaSalas.API.Repository.Interface;
using AgendaSalas.API.Service.Interface;
using AgendaSalas.Models.Dtos;

namespace AgendaSalas.API.Service;

public class AgendaService : IAgendaService
{
    private readonly IAgendaRepository _agendaRepository;

    public AgendaService(IAgendaRepository agendaRepository)
    {
        _agendaRepository = agendaRepository;
    }

    public async Task Create(AgendaDto agendaDto)
    {
        await _agendaRepository.Create(agendaDto.ConverterDtoParaAgenda());
    }

    public async Task Delete(int id)
    {
        var agendaEntity = await GetById(id);
        if (agendaEntity is not null)
        {
            await _agendaRepository.Delete(agendaEntity.Id);
        }
    }

    public async Task<IEnumerable<AgendaDto>> GetAll(int page, int size, string search)
    {
        var agendaEntity = await _agendaRepository.GetAll(page, size, search);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<AgendaDto>> GetByAgendaActive()
    {
        var agendaEntity = await _agendaRepository.GetByAgendaActive();
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<IEnumerable<AgendaDto>> GetByAgendaActiveSalaId(int salaId, DateTime dataSelecionada)
    {
        var agendaEntity = await _agendaRepository.GetByAgendaActiveSalaId(salaId, dataSelecionada);
        return agendaEntity.ConverterAgendasParaDto();
    }

    public async Task<AgendaDto> GetById(int id)
    {
        var agendaEntity = await _agendaRepository.GetById(id);
        return agendaEntity.ConverterAgendaParaDto();
    }

    public async Task<int> TotalAgendas(string search)
    {
        var totalAgenda = await _agendaRepository.TotalAgendas(search);
        return totalAgenda;
    }

    public async Task Update(AgendaDto agendaDto)
    {
        await _agendaRepository.Update(agendaDto.ConverterDtoParaAgenda());
    }
}
