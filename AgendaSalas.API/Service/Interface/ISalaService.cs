using AgendaSalas.Models.Dtos;

namespace AgendaSalas.API.Service.Interface;

public interface ISalaService
{
    Task<IEnumerable<SalaDto>> GetAll(int page, int size, string search);
    Task<SalaDto> GetById(int id);
    Task Create(SalaDto salaDto);
    Task Update(SalaDto salaDto);
    Task Delete(int id);
    Task<int> TotalSala(string search);
}
