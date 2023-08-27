using AgendaSalas.API.MappingDto;
using AgendaSalas.API.Repository.Interface;
using AgendaSalas.API.Service.Interface;
using AgendaSalas.Models.Dtos;

namespace AgendaSalas.API.Service;

public class SalaService : ISalaService
{
    private readonly ISalaRepository _salaRepository;

    public SalaService(ISalaRepository salaRepository)
    {
        _salaRepository = salaRepository;
    }

    public async Task Create(SalaDto salaDto)
    {
        await _salaRepository.Create(salaDto.ConverterDtoParaSala());
    }

    public async Task Delete(int id)
    {
        var salaEntity = await GetById(id);
        if (salaEntity is not null)
        {
            await _salaRepository.Delete(salaEntity.Id);
        }
    }

    public async Task<IEnumerable<SalaDto>> GetAll(int page, int size, string search)
    {
        var salaEntity = await _salaRepository.GetAll(page, size, search);
        return salaEntity.ConverterSalasParaDto();
    }

    public async Task<SalaDto> GetById(int id)
    {
        var salaEntity = await _salaRepository.GetById(id);
        return salaEntity.ConverterSalaParaDto();
    }

    public async Task<int> TotalSalas(string search)
    {
        var totalSala = await _salaRepository.TotalSalas(search);
        return totalSala;
    }

    public async Task Update(SalaDto salaDto)
    {
        await _salaRepository.Update(salaDto.ConverterDtoParaSala());
    }
}
