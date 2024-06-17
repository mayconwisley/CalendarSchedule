using ScheduleRooms.API.MappingDto.ScheduleUserDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class ScheduleUserService(IScheduleUserRepository scheduleUserRepository) : IScheduleUserService
{
    private readonly IScheduleUserRepository _scheduleUserRepository = scheduleUserRepository;

    public async Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserRepository.Create(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
        return scheduleUser.ConvertScheduleUserDto();
    }

    public async Task Delete(int scheduleId)
    {
        await _scheduleUserRepository.Delete(scheduleId);
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetAll(page, size, search);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByDateStart(int page, int size, DateTime dateStart)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDateStart(page, size, dateStart);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleId(int scheduleId)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByScheduleId(scheduleId);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }

    public async Task<int> TotalScheduleUser(string search)
    {
        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        return totalScheduleUser;
    }

    public async Task<ScheduleUserDto> Update(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserRepository.Update(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
        return scheduleUser.ConvertScheduleUserDto();
    }
}
