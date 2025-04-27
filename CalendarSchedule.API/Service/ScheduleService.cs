using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ScheduleDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ScheduleService(IScheduleRepository _scheduleUserRepository) : IScheduleService
{
    public async Task<Result<ScheduleDto>> Create(ScheduleCreateDto scheduleCreateDto)
    {
        var schedule = await _scheduleUserRepository.Create(scheduleCreateDto.ConvertDtoToScheduleCreate());
        if (schedule.Id == 0)
            return Result.Failure<ScheduleDto>(Error.Internal("Erro ao criar agenda"));

        var dto = schedule.ConvertScheduleToDto();

        return Result.Success(dto);
    }
    public async Task<Result> Delete(int id)
    {
        var scheduleEntity = await GetById(id);
        if (scheduleEntity.IsFailure)
            return Result.Failure<ScheduleDto>(scheduleEntity.Error);

        await _scheduleUserRepository.Delete(scheduleEntity.Value.Id);
        return Result.Success();

    }
    public async Task<Result<IEnumerable<ScheduleDto>>> GetAll(int page, int size, string search)
    {
        var scheduleEntity = await _scheduleUserRepository.GetAll(page, size, search);
        if (scheduleEntity is null)
            return Result.Failure<IEnumerable<ScheduleDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleEntity.ConvertSchedulesToDto();

        return Result.Success(dto);
    }
    public async Task<Result<ScheduleDto>> GetById(int id)
    {
        var scheduleEntity = await _scheduleUserRepository.GetById(id);
        if (scheduleEntity is null)
            return Result.Failure<ScheduleDto>(Error.NotFound("Nenhum agendamento encontrado"));
        var dto = scheduleEntity.ConvertScheduleToDto();

        return Result.Success(dto);
    }
    public async Task<Result<int>> TotalSchedules(string search)
    {
        var totalScheduleUser = await _scheduleUserRepository.TotalSchedules(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

        return Result.Success(totalScheduleUser);
    }
    public async Task<Result<ScheduleDto>> Update(ScheduleCreateDto scheduleCreateDto)
    {
        var schedule = await _scheduleUserRepository.Update(scheduleCreateDto.ConvertDtoToScheduleCreate());
        if (schedule is null)
            return Result.Failure<ScheduleDto>(Error.Internal("Erro ao atualizar agenda"));

        var dto = schedule.ConvertScheduleToDto();

        return Result.Success(dto);
    }
}
