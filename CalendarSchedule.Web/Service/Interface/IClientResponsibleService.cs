using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IClientResponsibleService
{
	Task<Result<PagedResultView<ClientResponsibleDto>>> GetAll(int page = 1, int size = 10, string search = "");
	Task<Result<ClientResponsibleDto>> GetById(int id);
	Task<Result<ClientResponsibleDto>> Create(ClientResponsibleCreateDto clientResponsibleCreateDto);
	Task<Result<ClientResponsibleDto>> Update(ClientResponsibleDto clientResponsibleDto);
	Task<Result<bool>> Delete(int id);
}
