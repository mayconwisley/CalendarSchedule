using CalendarSchedule.API.MappingDto.UserContatctDto;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class UserContactService(IUserContactRepository _userContactRepository) : IUserContactService
{
	public async Task<Result<UserContactDto>> Create(UserContactCreateDto userContactCreateDto)
	{
		var user = await _userContactRepository.Create(userContactCreateDto.ConvertDtoToUserContactCreate());
		if (user.Id == 0)
			return Result.Failure<UserContactDto>(Error.BadRequest("Erro ao cadastrar contato do usuário"));

		var dto = await GetById(user.Id);
		var dtoResult = dto.Value;
		return Result.Success(dtoResult);
	}

	public async Task<Result<UserContactDto>> Delete(int id)
	{
		var deletedUserContact = await _userContactRepository.Delete(id);
		if (deletedUserContact is null)
			return Result.Failure<UserContactDto>(Error.NotFound("Contato não encontrado"));
		var dto = deletedUserContact.ConvertUserContactToDto();
		return Result.Success(dto);
	}

	public async Task<Result<PagedResult<UserContactDto>>> GetAll(int page, int size, string search)
	{
		var userContacts = await _userContactRepository.GetAll(page, size, search);
		if (!userContacts.Any())
			return Result.Failure<PagedResult<UserContactDto>>(Error.NotFound("Contato não encontrado"));

		var totalUserContact = await _userContactRepository.TotalUserContact(search);
		if (totalUserContact <= 0)
			return Result.Failure<PagedResult<UserContactDto>>(Error.NotFound("Nenhum contato encontrado"));

		decimal totalData = totalUserContact;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = userContacts.ConvertUserContactsToDto();
		var usercContactDto = new PagedResult<UserContactDto>(dto, totalData, page, totalPage, size);

		return Result.Success(usercContactDto);
	}

	public async Task<Result<UserContactDto>> GetById(int id)
	{
		var userContact = await _userContactRepository.GetById(id);
		if (userContact is null)
			return Result.Failure<UserContactDto>(Error.NotFound("Contato não encontrado"));
		var dto = userContact.ConvertUserContactToDto();
		return Result.Success(dto);
	}

	public async Task<Result<PagedResult<UserContactDto>>> GetByUserId(int page, int size, int userId)
	{
		var userContacts = await _userContactRepository.GetByUserId(page, size, userId);
		if (!userContacts.Any())
			return Result.Failure<PagedResult<UserContactDto>>(Error.NotFound("Contato não encontrado"));


		var totalUserContact = await _userContactRepository.TotalUserContact(userId.ToString());
		if (totalUserContact <= 0)
			return Result.Failure<PagedResult<UserContactDto>>(Error.NotFound("Nenhum contato encontrado"));

		decimal totalData = totalUserContact;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = userContacts.ConvertUserContactsToDto();
		var userContactDto = new PagedResult<UserContactDto>(dto, totalData, page, totalPage, size);

		return Result.Success(userContactDto);
	}

	public async Task<Result<int>> TotalUserContact(string search)
	{
		var totalUserContact = await _userContactRepository.TotalUserContact(search);
		if (totalUserContact <= 0)
			return Result.Failure<int>(Error.NotFound("Nenhum contato encontrado"));

		return Result.Success(totalUserContact);
	}

	public async Task<Result<UserContactDto>> Update(UserContactDto userContactDto)
	{
		var userContact = await _userContactRepository.Update(userContactDto.ConvertDtoToUserContact());
		if (userContact is null)
			return Result.Failure<UserContactDto>(Error.NotFound("Contato não encontrado"));

		var dto = userContact.ConvertUserContactToDto();
		return Result.Success(dto);
	}
}
