using CalendarSchedule.API.MappingDto.ClientContactDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class ClientContactService(IClientContactRepository _clientContactRepository) : IClientContactService
{
	public async Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto)
	{
		var clientContact = await _clientContactRepository.Create(clientContactCreateDto.ConvertDtoToClientContactCreate());
		if (clientContact.Id == 0)
			return Result.Failure<ClientContactDto>(Error.Internal("Erro ao criar contato"));

		var dto = await GetById(clientContact.Id);
		var dtoResult = dto.Value;

		return Result.Success(dtoResult);
	}

	public async Task<Result<ClientContactDto>> Delete(int id)
	{
		var deleted = await _clientContactRepository.Delete(id);
		if (deleted is null)
			return Result.Failure<ClientContactDto>(Error.NotFound("Nenhum contato encontrado para ser excluido"));

		var deletedClientContact = deleted.ConvertClientContactToDto();

		return Result.Success(deletedClientContact);
	}

	public async Task<Result<PagedResult<ClientContactDto>>> GetAll(int page, int size, string search)
	{
		var clientContacts = await _clientContactRepository.GetAll(page, size, search);
		if (clientContacts is null)
			return Result.Failure<PagedResult<ClientContactDto>>(Error.NotFound("Contatos não encontrado"));

		var totalClientContact = await _clientContactRepository.TotalClientContact(search);
		if (totalClientContact <= 0)
			return Result.Failure<PagedResult<ClientContactDto>>(Error.NotFound("Contatos não encontrado"));

		decimal totalData = totalClientContact;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
		if (size == 1)
			totalPage = totalData;

		var dto = clientContacts!.ConvertClientContactsToDto();
		var clientContactDto = new PagedResult<ClientContactDto>(dto, totalData, page, totalPage, size);

		return Result.Success(clientContactDto);
	}

	public async Task<Result<PagedResult<ClientContactDto>>> GetByClientId(int page, int size, int clientId)
	{
		var clientContacts = await _clientContactRepository.GetByClientId(page, size, clientId);
		if (clientContacts is null)
			return Result.Failure<PagedResult<ClientContactDto>>(Error.NotFound("Nenhum contato encontrado"));

		var totalClientContact = await _clientContactRepository.TotalClientContact(clientId.ToString());
		if (totalClientContact <= 0)
			return Result.Failure<PagedResult<ClientContactDto>>(Error.NotFound("Nenhum contato encontrado"));

		decimal totalData = totalClientContact;
		decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);

		if (size == 1)
			totalPage = totalData;

		var dto = clientContacts!.ConvertClientContactsToDto();
		var clientContactDto = new PagedResult<ClientContactDto>(dto, totalData, page, totalPage, size);
		return Result.Success(clientContactDto);
	}

	public async Task<Result<ClientContactDto>> GetById(int id)
	{
		var clientContact = await _clientContactRepository.GetById(id);
		if (clientContact is null)
			return Result.Failure<ClientContactDto>(Error.NotFound("Nenhum contato encontrado"));

		var dto = clientContact.ConvertClientContactToDto();
		return Result.Success(dto);
	}

	public async Task<Result<int>> TotalClientContact(string search)
	{
		var totalClientContact = await _clientContactRepository.TotalClientContact(search);
		if (totalClientContact <= 0)
			return Result.Failure<int>(Error.BadRequest("Erro ao totalizar contato"));

		return Result.Success(totalClientContact);
	}

	public async Task<Result<ClientContactDto>> Update(ClientContactUpdateDto clientContactUpdateDto)
	{
		var clientContact = await _clientContactRepository.Update(clientContactUpdateDto.ConvertDtoToClientContactUpdate());
		if (clientContact is null)
			return Result.Failure<ClientContactDto>(Error.Internal("Erro ao atualizar contato"));

		var dto = clientContact.ConvertClientContactToDto();
		return Result.Success(dto);
	}
}
