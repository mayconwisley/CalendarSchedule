using CalendarSchedule.API.MappingDto.ScheduleUserDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ScheduleUserService(IScheduleUserRepository _scheduleUserRepository) : IScheduleUserService
{
	public async Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto)
	{
		var scheduleUser = await _scheduleUserRepository.Create(scheduleUserCreateDto.ConvertDtoScheduleUserCreate());
		var dto = await GetById(scheduleUser.ScheduleId, scheduleUser.UserId);

		return Result.Success(dto.Value);
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
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		var totalScheduleUser = await TotalScheduleUser(search);
		if (totalScheduleUser.Value <= 0)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		decimal totalData = totalScheduleUser.Value;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = scheduleUserDtos.ConvertSchedulesUserDto();

		var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, totalPage, size);

		return Result.Success(scheduleUser);
	}
	public async Task<Result<PagedResult<ScheduleUserDto>>> GetByDatePeriod(int page, int size, string search, DateOnly dateStart, DateOnly dateEnd)
	{
		var dtDateStart = DateTime.Parse(dateStart.ToString());
		var dtDateEnd = DateTime.Parse(dateEnd.ToString());

		var scheduleUserDtos = await _scheduleUserRepository.GetByDatePeriod(page, size, search, dtDateStart, dtDateEnd);
		if (scheduleUserDtos is null)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		var totalScheduleUser = await TotalScheduleUser(dtDateStart, dtDateEnd, search);
		if (totalScheduleUser.Value <= 0)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		decimal totalData = totalScheduleUser.Value;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = scheduleUserDtos.ConvertSchedulesUserDto();

		var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, totalPage, size);

		return Result.Success(scheduleUser);
	}
	public async Task<Result<PagedResult<ScheduleUserDto>>> GetByDateStart(int page, int size, string search, DateOnly dateStart)
	{
		var dtDateStart = DateTime.Parse(dateStart.ToString());

		var scheduleUserDtos = await _scheduleUserRepository.GetByDateStart(page, size, search, dtDateStart);
		if (scheduleUserDtos is null)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		var totalScheduleUser = await TotalScheduleUser(dtDateStart, search);
		if (totalScheduleUser.Value <= 0)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));
		decimal totalData = totalScheduleUser.Value;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = scheduleUserDtos.ConvertSchedulesUserDto();
		var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, totalPage, size);

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
		var scheduleUserDtos = await _scheduleUserRepository.GetByScheduleId(page, size, search, scheduleId);
		if (scheduleUserDtos is null)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos nãoencontrado"));

		var totalScheduleUser = await TotalScheduleUser(scheduleId, search);
		if (totalScheduleUser.Value <= 0)
			return Result.Failure<PagedResult<ScheduleUserDto>>(Error.NotFound("Agendamentos não encontrado"));

		decimal totalData = totalScheduleUser.Value;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);

		if (size == 1)
			totalPage = totalData;

		var dto = scheduleUserDtos.ConvertSchedulesUserDto();
		var scheduleUser = new PagedResult<ScheduleUserDto>(dto, totalData, page, totalPage, size);

		return Result.Success(scheduleUser);
	}
	public async Task<Result<int>> TotalScheduleUser(string search)
	{
		var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(search);
		if (totalScheduleUser <= 0)
			return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

		return Result.Success(totalScheduleUser);
	}

	public async Task<Result<int>> TotalScheduleUser(int scheduleId, string search)
	{
		var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(scheduleId, search);
		if (totalScheduleUser <= 0)
			return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

		return Result.Success(totalScheduleUser);
	}

	public async Task<Result<int>> TotalScheduleUser(DateTime dateStart, DateTime dateEnd, string search)
	{
		var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(dateStart, dateEnd, search);
		if (totalScheduleUser <= 0)
			return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

		return Result.Success(totalScheduleUser);
	}

	public async Task<Result<int>> TotalScheduleUser(DateTime dateStart, string search)
	{
		var totalScheduleUser = await _scheduleUserRepository.TotalScheduleUser(dateStart, search);
		if (totalScheduleUser <= 0)
			return Result.Failure<int>(Error.NotFound("Nenhum agendamento encontrado"));

		return Result.Success(totalScheduleUser);
	}
}
