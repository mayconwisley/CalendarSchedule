using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IClientContactService
{
	Task<Result<PagedResultView<ClientContactDto>>> GetAll(int page = 1, int size = 10, string search = "");
	Task<Result<PagedResultView<ClientContactDto>>> GetByClientId(int page = 1, int size = 10, int userId = 0);
	Task<Result<ClientContactDto>> GetById(int id);
	Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto);
	Task<Result<ClientContactDto>> Update(ClientContactUpdateDto clientContactUpdateDto);
	Task<Result<bool>> Delete(int id);
}
