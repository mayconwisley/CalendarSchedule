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
    public async Task<Result<ScheduleUserDto>> Delete(int scheduleId, int userId)
    {
        var deletedScheduleUser = await _scheduleUserRepository.Delete(scheduleId, userId);
        if (deletedScheduleUser is null)
            return Result.Failure<ScheduleUserDto>(Error.NotFound("Nenhum agendamento encontrado para ser excluido"));

        var dto = deletedScheduleUser.ConvertScheduleUserDto();

        return Result.Success(dto);
    }
    public async Task<Result<PagedResult<ScheduleUserDto>>> GetAll(int page, int size, string search)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetAll(page, size, search);
        if (scheduleUserDtos is null)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        decimal totalData = totalScheduleUser;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
        if (size == 1)
            totalPage = totalData;

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();

        var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, size, totalPage);

        return Result.Success(scheduleUser);
    }
    public async Task<Result<PagedResult<ScheduleUserDto>>> GetByDatePeriod(int page, int size, string search, DateTime dateStart, DateTime dateEnd)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDatePeriod(dateStart, dateEnd);
        if (scheduleUserDtos is null)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        decimal totalData = totalScheduleUser;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
        if (size == 1)
            totalPage = totalData;

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();

        var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, size, totalPage);

        return Result.Success(scheduleUser);
    }
    public async Task<Result<PagedResult<ScheduleUserDto>>> GetByDateStart(int page, int size, string search, DateTime dateStart)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByDateStart(dateStart);
        if (scheduleUserDtos is null)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));
        decimal totalData = totalScheduleUser;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
        if (size == 1)
            totalPage = totalData;

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();
        var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, size, totalPage);

        return Result.Success(scheduleUser);
    }
    public async Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId)
    {
        var scheduleUserDto = await _scheduleUserRepository.GetById(scheduleId, userId);
        if (scheduleUserDto is null)
            return Result.Failure<ScheduleUserDto>(Error.NotFound("Nenhum agendamento encontrado"));

        var dto = scheduleUserDto.ConvertScheduleUserDto();

        return Result.Success(dto);
    }
    public async Task<Result<PagedResult<ScheduleUserDto>>> GetByScheduleId(int page, int size, string search, int scheduleId)
    {
        var scheduleUserDtos = await _scheduleUserRepository.GetByScheduleId(scheduleId);
        if (scheduleUserDtos is null)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Nenhum agendamento encontrado"));

        decimal totalData = totalScheduleUser;
        decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));

        if (size == 1)
            totalPage = totalData;

        var dto = scheduleUserDtos.ConvertSchedulesUserDto();
        var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, size, totalPage);

        return Result.Success(scheduleUser);
    }
    public async Task<Result<int>> TotalScheduleUser(string search)
    {
        var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
        if (totalScheduleUser <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

        return Result.Success(totalScheduleUser);
    }
    public async Task<Result<ScheduleUserDto>> Update(ScheduleUserDto scheduleUserDto)
    {
        var scheduleUser = await _scheduleUserRepository.Update(scheduleUserDto.ConvertDtoScheduleUser());
        if (scheduleUser is null)
            return Result.Failure<ScheduleUserDto>(Error.Internal("Erro ao atualizar agendamento"));

        var dto = scheduleUser.ConvertScheduleUserDto();
        return Result.Success(dto);
    }
}
