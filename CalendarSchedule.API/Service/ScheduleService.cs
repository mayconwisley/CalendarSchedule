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

		var dto = await GetById(schedule.Id);
		var dtoResult = dto.Value;

		return Result.Success(dtoResult);
	}
	public async Task<Result<ScheduleDto>> Delete(int id)
	{
		var deletedSchedule = await _scheduleUserRepository.Delete(id);
		if (deletedSchedule is null)
			return Result.Failure<ScheduleDto>(Error.NotFound("Nenhum agendamento encontrado para ser excluido"));
		var dto = deletedSchedule.ConvertScheduleToDto();

		return Result.Success(dto);
	}
	public async Task<Result<PagedResult<ScheduleDto>>> GetAll(int page, int size, string search)
	{
		var scheduleEntity = await _scheduleUserRepository.GetAll(page, size, search);
		if (scheduleEntity is null)
			return Result.Failure<PagedResult<ScheduleDto>>(Error.NotFound("Nenhum agendamento encontrado"));

		var totalSchedule = await _scheduleUserRepository.TotalSchedules(search);
		if (totalSchedule <= 0)
			return Result.Failure<PagedResult<ScheduleDto>>(Error.NotFound("Nenhum agendamento encontrado"));

		decimal totalData = totalSchedule;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling((totalData / size));
		if (size == 1)
			totalPage = totalData;

		var dto = scheduleEntity.ConvertSchedulesToDto();
		var scheduleDto = new PagedResult<ScheduleDto>(dto, totalData, page, totalPage, size);

		return Result.Success(scheduleDto);
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
	public async Task<Result<ScheduleDto>> Update(ScheduleUpdateDto scheduleUpdateDto)
	{
		var schedule = await _scheduleUserRepository.Update(scheduleUpdateDto.ConvertDtoToScheduleUpdate());
		if (schedule is null)
			return Result.Failure<ScheduleDto>(Error.Internal("Erro ao atualizar agenda"));

		var dto = schedule.ConvertScheduleToDto();

		return Result.Success(dto);
	}
}
