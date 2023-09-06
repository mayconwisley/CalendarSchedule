using AgendaSalas.Models.Dtos;

namespace AgendaSalas.API.Service.Interface;

public interface IAgendaService
{
    Task<IEnumerable<AgendaDto>> GetAll(int page, int size, string search);
    Task<IEnumerable<AgendaDto>> GetByAgendaActive();
    Task<IEnumerable<AgendaDto>> GetByAgendaActiveSalaId(int salaId, DateTime dataSelecionada);
    Task<AgendaDto> GetById(int id);
    Task Create(AgendaDto agendaDto);
    Task Update(AgendaDto agendaDto);
    Task Delete(int id);
    Task<int> TotalAgendas(string search);
}
