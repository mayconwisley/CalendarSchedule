using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ScheduleUserDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ScheduleUserService(IScheduleUserRepository _scheduleUserRepository) : IScheduleUserService
{
    public async Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserRepository.Create(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
        if (scheduleUser.UserId == 0)
            return Result.Failure<ScheduleUserDto>(Error.Internal("Erro ao criar agendamento"));

        var dto = scheduleUser.ConvertScheduleUserDto();

        return Result.Success(dto);
    }
    public async Task<Result> Delete(int scheduleId, int userId)
    {
        var result = await GetById(scheduleId, userId);
        if (result.IsFailure)
            return Result.Failure<ScheduleUserDto>(result.Error);

        await _scheduleUserRepository.Delete(result.Value.ScheduleId, result.Value.UserId);

        return Result.Success();
    }
    public async Task<Result<IEnumerable<ScheduleUserDto>>> GetAll(int page, int size, string search)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetAll(page, size, search);
        if (scheduleUserDtos is null)
            return Result.Failure<IEnumerable<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();

        return Result.Success(dto);
    }

    public async Task<Result<IEnumerable<ScheduleUserDto>>> GetByDatePeriod(DateTime dateStart, DateTime dateEnd)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDatePeriod(dateStart, dateEnd);
        if (scheduleUserDtos is null)
            return Result.Failure<IEnumerable<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();

        return Result.Success(dto);
    }

    public async Task<Result<IEnumerable<ScheduleUserDto>>> GetByDateStart(DateTime dateStart)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDateStart(dateStart);
        if (scheduleUserDtos is null)
            return Result.Failure<IEnumerable<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();
        return Result.Success(dto);
    }
    public async Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserRepository.GetById(scheduleId, userId);
        if (scheduleUserDto is null)
            return Result.Failure<ScheduleUserDto>(Error.NotFound("Nenhum agendamento encontrado"));
        var dto = scheduleUserDto.ConvertScheduleUserDto();

        return Result.Success(dto);
    }
    public async Task<Result<IEnumerable<ScheduleUserDto>>> GetByScheduleId(int scheduleId)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByScheduleId(scheduleId);
        if (scheduleUserDtos is null)
            return Result.Failure<IEnumerable<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();
        return Result.Success(dto);
    }
    public async Task<Result<int>> TotalScheduleUser(string search)
    {
        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

        return Result.Success(totalScheduleUser);
    }
    public async Task<Result<ScheduleUserDto>> Update(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        var scheduleUser = await _scheduleUserRepository.Update(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
        if (scheduleUser is null)
            return Result.Failure<ScheduleUserDto>(Error.Internal("Erro ao atualizar agendamento"));

        var dto = scheduleUser.ConvertScheduleUserDto();
        return Result.Success(dto);
    }
}
