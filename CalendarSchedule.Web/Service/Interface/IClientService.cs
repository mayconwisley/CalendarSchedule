using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IClientService
{
    Task<Result<PagedResultView<ClientDto>>> GetAll(int page = 1, int size = 10, string search = "");
    Task<Result<ClientDto>> GetById(int id);
    Task<Result<ClientDto>> Create(ClientDto clientDto);
    Task<Result<ClientDto>> Update(ClientDto clientDto);
    Task<Result<bool>> Delete(int id);
}
