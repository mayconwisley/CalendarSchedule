using CalendarSchedule.API.Model;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.MappingDto.UserContatctDto;

public static class UserContactContactMappingDto
{
	public static IEnumerable<UserContactDto> ConvertUserContactsToDto(this IEnumerable<UserContact> userContacts)
	{
		return userContacts.Select(s => s.ConvertUserContactToDto());
	}
	public static IEnumerable<UserContact> ConvertDtoToUserContacts(this IEnumerable<UserContactDto> userContactDtos)
	{
		return userContactDtos.Select(s => s.ConvertDtoToUserContact());
	}
	public static UserContactDto ConvertUserContactToDto(this UserContact userContact)
	{
		return new UserContactDto
		{
			Id = userContact.Id,
			Number = userContact.Number,
			Type = userContact.Type,
			UserId = userContact.UserId,
			UserDto = new()
			{
				Id = userContact.User.Id,
				Active = userContact.User.Active,
				Description = userContact.User.Description,
				Manager = userContact.User.Manager,
				Name = userContact.User.Name,
				Username = userContact.User.Username,
				Password = userContact.User.Password
			}

		};
	}
	public static UserContact ConvertDtoToUserContact(this UserContactDto userContactDto)
	{
		return new UserContact
		{
			Id = userContactDto.Id,
			Number = userContactDto.Number,
			Type = userContactDto.Type,
			UserId = userContactDto.UserId,
			User = new()
			{
				Id = userContactDto.UserDto.Id,
				Active = userContactDto.UserDto.Active,
				Description = userContactDto.UserDto.Description,
				Manager = userContactDto.UserDto.Manager,
				Name = userContactDto.UserDto.Name,
				Username = userContactDto.UserDto.Username,
				Password = userContactDto.UserDto.Password
			}
		};
	}
	public static UserContactCreateDto ConvertUserContactCreateToDto(this UserContact userContact)
	{
		return new UserContactCreateDto
		{
			Number = userContact.Number,
			Type = userContact.Type,
			UserId = userContact.UserId,
		};
	}
	public static UserContact ConvertDtoToUserContactCreate(this UserContactCreateDto userContactCreateDto)
	{
		return new UserContact
		{
			Number = userContactCreateDto.Number,
			Type = userContactCreateDto.Type,
			UserId = userContactCreateDto.UserId
		};
	}
}
