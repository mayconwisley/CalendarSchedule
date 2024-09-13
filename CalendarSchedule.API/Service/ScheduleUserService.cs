using CalendarSchedule.API.MappingDto.ScheduleUserDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ScheduleUserService(IScheduleUserRepository scheduleUserRepository) : IScheduleUserService
{
    private readonly IScheduleUserRepository _scheduleUserRepository = scheduleUserRepository;
    public async Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserRepository.Create(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
        return scheduleUser.ConvertScheduleUserDto();
    }
    public async Task Delete(int scheduleId, int userId)
    {
        await _scheduleUserRepository.Delete(scheduleId, userId);
    }
    public async Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetAll(page, size, search);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByDatePeriod(DateTime dateStart, DateTime dateEnd)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDatePeriod(dateStart, dateEnd);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByDateStart(DateTime dateStart)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDateStart(dateStart);
        return scheduleUserDtos.ConvertSchedulesUserDto();
    }
    public async Task<ScheduleUserDto> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserRepository.GetById(scheduleId, userId);
        return scheduleUserDto.ConvertScheduleUserDto();
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
