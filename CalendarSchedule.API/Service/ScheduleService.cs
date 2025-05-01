using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.ScheduleDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ScheduleService(IScheduleRepository _scheduleUserRepository, IClientRepository _clientRepository) : IScheduleService
{
	public async Task<Result<ScheduleDto>> Create(ScheduleCreateDto scheduleCreateDto)
	{
		var clientId = scheduleCreateDto.ClientId;

		if (!ValidateDatesCreate(scheduleCreateDto))
			return Result.Failure<ScheduleDto>(Error.BadRequest("Data inicial deve ser menor que a data final"));

		if (IsParticularCreate(scheduleCreateDto) && clientId == 0)
			return Result.Failure<ScheduleDto>(Error.BadRequest("Agenda particular, remova o campo ClienteId"));

		if (!IsParticularCreate(scheduleCreateDto) && (clientId == 0 || clientId is null))
			return Result.Failure<ScheduleDto>(Error.BadRequest("Agenda não é particular, obrigatório preencher ClienteId"));

		if (clientId is not null)
		{
			var client = await _clientRepository.GetById((int)clientId);
			if (client is null)
				return Result.Failure<ScheduleDto>(Error.BadRequest("Cliente não existe"));
		}

		var scheduleExists = await ExistsOverlapCreate(scheduleCreateDto);
		if (scheduleExists)
			return Result.Failure<ScheduleDto>(Error.Conflict($"Já existe um agendamento nesse horário para este cliente {scheduleCreateDto.ClientId}"));

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

	public async Task<bool> ExistsOverlapCreate(ScheduleCreateDto scheduleCreateDto) =>
		await _scheduleUserRepository.ExistsOverlap(scheduleCreateDto.ConvertDtoToScheduleCreate());

	public async Task<bool> ExistsOverlapUpdate(ScheduleUpdateDto scheduleUpdateDto) =>
		await _scheduleUserRepository.ExistsOverlap(scheduleUpdateDto.ConvertDtoToScheduleUpdate());

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

	public bool IsParticularCreate(ScheduleCreateDto scheduleCreateDto)
	{
		if (scheduleCreateDto.Particular && (scheduleCreateDto.ClientId is null || scheduleCreateDto.ClientId == 0))
			return true;
		return false;
	}

	public bool IsParticularUpdate(ScheduleUpdateDto scheduleUpdateDto)
	{
		if (scheduleUpdateDto.Particular && (scheduleUpdateDto.ClientId is null || scheduleUpdateDto.ClientId == 0))
			return true;
		return false;
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
		var clientId = scheduleUpdateDto.ClientId;

		var scheduleUpdate = await GetById(scheduleUpdateDto.Id);
		var schedule = scheduleUpdate.Value;

		if (!ValidateDatesUpdate(scheduleUpdateDto))
			return Result.Failure<ScheduleDto>(Error.BadRequest("Data inicial deve ser menor que a data final"));

		if (scheduleUpdateDto.Particular != schedule.Particular)
		{
			if (!IsParticularUpdate(scheduleUpdateDto) && (clientId == 0 || clientId is null))
				return Result.Failure<ScheduleDto>(Error.BadRequest("Agenda não é particular, obrigatório preencher ClienteId"));

			if (IsParticularUpdate(scheduleUpdateDto) && clientId == 0)
				return Result.Failure<ScheduleDto>(Error.BadRequest("Agenda particular, remova o campo ClienteId"));
		}

		if (clientId is not null)
		{
			var client = await _clientRepository.GetById((int)clientId);
			if (client is null)
				return Result.Failure<ScheduleDto>(Error.BadRequest("Cliente não existe"));
		}

		var scheduleExists = await ExistsOverlapUpdate(scheduleUpdateDto);
		if (scheduleExists)
			return Result.Failure<ScheduleDto>(Error.Conflict($"Já existe um agendamento nesse horário para este cliente: {scheduleUpdateDto.ClientId}"));

		var entity = await _scheduleUserRepository.Update(scheduleUpdateDto.ConvertDtoToScheduleUpdate());
		if (entity is null)
			return Result.Failure<ScheduleDto>(Error.Internal("Erro ao atualizar agenda"));

		var dto = entity.ConvertScheduleToDto();

		return Result.Success(dto);
	}

	public bool ValidateDatesCreate(ScheduleCreateDto scheduleCreateDto) =>
		scheduleCreateDto.DateStart < scheduleCreateDto.DateFinal;

	public bool ValidateDatesUpdate(ScheduleUpdateDto scheduleUpdateDto) =>
		scheduleUpdateDto.DateStart < scheduleUpdateDto.DateFinal;
}
