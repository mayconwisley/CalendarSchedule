using AgendaSalas.API.Model;

namespace AgendaSalas.API.Repository.Interface;

public interface ISalaRepository
{
    Task<IEnumerable<Sala>> GetAll(int page, int size, string search);
    Task<Sala> GetById(int id);
    Task<Sala> Create(Sala sala);
    Task<Sala> Update(Sala sala);
    Task<Sala> Delete(int id);
    Task<int> TotalSalas(string search);
}
