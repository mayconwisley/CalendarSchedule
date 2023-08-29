using AgendaSalas.Models.Dtos;
using AgendaSalas.Web.Models;

namespace AgendaSalas.Web.Service;

public interface ISalaService
{
    Task<SalaView> GetAll(int page = 1, int size = 25, string search = "");
    Task<SalaDto> GetById(int id);
    Task<SalaDto> Create(SalaDto salaDto);
    Task<SalaDto> Update(SalaDto salaDto);
    Task<bool> Delete(int id);
}
