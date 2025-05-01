using CalendarSchedule.API.MappingDto.ClientDtos;
using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.ScheduleDtos;

public static class ScheduleMappingDto
{
	public static IEnumerable<ScheduleDto> ConvertSchedulesToDto(this IEnumerable<Schedule> schedules)
	{
		return schedules.Select(s => s.ConvertScheduleToDto());
	}
	public static IEnumerable<Schedule> ConvertScheduleToDto(this IEnumerable<ScheduleDto> scheduleDtos)
	{
		return scheduleDtos.Select(s => s.ConvertDtoToSchedule());
	}
	public static ScheduleDto ConvertScheduleToDto(this Schedule schedule)
	{
		return new ScheduleDto
		{
			Id = schedule.Id,
			Description = schedule.Description,
			DateStart = schedule.DateStart,
			DateFinal = schedule.DateFinal,
			MeetingType = schedule.MeetingType,
			StatusSchedule = schedule.StatusSchedule,
			ClientId = schedule?.ClientId,
			ClientDto = schedule.Client == null ? null : schedule.Client.ConvertClientToDto(),
			Particular = schedule.Particular,
		};
	}
	public static Schedule ConvertDtoToSchedule(this ScheduleDto scheduleDto)
	{
		return new Schedule
		{
			Id = scheduleDto.Id,
			Description = scheduleDto.Description,
			DateStart = scheduleDto.DateStart,
			DateFinal = scheduleDto.DateFinal,
			MeetingType = scheduleDto.MeetingType,
			StatusSchedule = scheduleDto.StatusSchedule,
			ClientId = scheduleDto?.ClientId,
			Client = scheduleDto.ClientDto == null ? null : scheduleDto.ClientDto.ConvertDtoToClient(),
			Particular = scheduleDto.Particular,
		};
	}

	public static Schedule ConvertDtoToScheduleUpdate(this ScheduleUpdateDto scheduleUpdateDto)
	{
		return new Schedule
		{
			Description = scheduleUpdateDto.Description,
			DateStart = scheduleUpdateDto.DateStart,
			DateFinal = scheduleUpdateDto.DateFinal,
			MeetingType = scheduleUpdateDto.MeetingType,
			StatusSchedule = scheduleUpdateDto.StatusSchedule,
			ClientId = scheduleUpdateDto?.ClientId,
			Particular = scheduleUpdateDto.Particular,
		};
	}
	public static Schedule ConvertDtoToScheduleCreate(this ScheduleCreateDto scheduleCreateDto)
	{
		return new Schedule
		{
			Description = scheduleCreateDto.Description,
			DateStart = scheduleCreateDto.DateStart,
			DateFinal = scheduleCreateDto.DateFinal,
			MeetingType = scheduleCreateDto.MeetingType,
			StatusSchedule = scheduleCreateDto.StatusSchedule,
			ClientId = scheduleCreateDto?.ClientId,
			Particular = scheduleCreateDto.Particular,
		};
	}
}
