using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;

namespace CalendarSchedule.Web.Service.Interface;

public interface IClientResponsibleService
{
    Task<ClientResponsibleView> GetAll(int page = 1, int size = 10, string search = "");
    Task<ClientResponsibleDto> GetById(int id);
    Task<ClientResponsibleDto> Create(ClientResponsibleCreateDto clientResponsibleCreateDto);
    Task<ClientResponsibleDto> Update(ClientResponsibleCreateDto clientResponsibleCreateDto);
    Task<bool> Delete(int id);
}
