using AgendaSalas.API.Model;

namespace AgendaSalas.API.Repository.Interface;

public interface IAgendaRepository
{
    Task<IEnumerable<Agenda>> GetAll(int page, int size, string search);
    Task<IEnumerable<Agenda>> GetByAgendaActive();
    Task<IEnumerable<Agenda>> GetByAgendaActiveSalaId(int salaId, DateTime dataSelecionada);
    Task<Agenda> GetById(int id);
    Task<Agenda> Create(Agenda agenda);
    Task<Agenda> Update(Agenda agenda);
    Task<Agenda> Delete(int id);
    Task<int> TotalAgendas(string search);
}
