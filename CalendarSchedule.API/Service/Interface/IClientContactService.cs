using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IClientContactService
{
	Task<Result<PagedResult<ClientContactDto>>> GetAll(int page, int size, string search);
	Task<Result<PagedResult<ClientContactDto>>> GetByClientId(int page, int size, int clientId);
	Task<Result<ClientContactDto>> GetById(int id);
	Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto);
	Task<Result<ClientContactDto>> Update(ClientContactUpdateDto clientContactUpdateDto);
	Task<Result<ClientContactDto>> Delete(int id);
	Task<Result<int>> TotalClientContact(string search);
}
