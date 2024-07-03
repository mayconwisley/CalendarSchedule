using ScheduleRooms.API.MappingDto.ScheduleDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ScheduleService(IScheduleRepository scheduleUserRepository) : IScheduleService
{
    private readonly IScheduleRepository _scheduleUserRepository = scheduleUserRepository;

    public async Task<ScheduleDto> Create(ScheduleCreateDto scheduleCreateDto)
    {
        var schedule = await _scheduleUserRepository.Create(scheduleCreateDto.ConvertDtoToScheduleCreate());
        return schedule.ConvertScheduleToDto();
    }

    public async Task Delete(int id)
    {
        var scheduleEntity = await GetById(id);
        if (scheduleEntity is not null)
        {
            await _scheduleUserRepository.Delete(scheduleEntity.Id);
        }
    }

    public async Task<IEnumerable<ScheduleDto>> GetAll(int page, int size, string search)
    {
        var scheduleEntity = await _scheduleUserRepository.GetAll(page, size, search);
        return scheduleEntity.ConvertSchedulesToDto();
    }

    public async Task<ScheduleDto> GetById(int id)
    {
        var scheduleEntity = await _scheduleUserRepository.GetById(id);
        return scheduleEntity.ConvertScheduleToDto();
    }

    public async Task<IEnumerable<ScheduleDto>> GetBySchedule()
    {
        var schedyleEntity = await _scheduleUserRepository.GetBySchedule();
        return schedyleEntity.ConvertSchedulesToDto();
    }

    public async Task<IEnumerable<ScheduleDto>> GetByScheduleActive()
    {
        var schedyleEntity = await _scheduleUserRepository.GetByScheduleActive();
        return schedyleEntity.ConvertSchedulesToDto();
    }

    public async Task<IEnumerable<ScheduleDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
    {
        var scheduleEntity = await _scheduleUserRepository.GetByScheduleActiveClientId(clientId, dateSalected);
        return scheduleEntity.ConvertSchedulesToDto();
    }

    public async Task<int> TotalSchedules(string search)
    {
        var totalScheduleUser = await _scheduleUserRepository.TotalSchedules(search);
        return totalScheduleUser;
    }

    public async Task<ScheduleDto> Update(ScheduleCreateDto scheduleCreateDto)
    {
        var schedule = await _scheduleUserRepository.Update(scheduleCreateDto.ConvertDtoToScheduleCreate());
        return schedule.ConvertScheduleToDto();
    }
}
