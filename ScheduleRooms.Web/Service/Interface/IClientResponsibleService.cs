using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;

namespace ScheduleRooms.Web.Service.Interface;

public interface IClientResponsibleService
{
    Task<ClientResponsibleView> GetAll(int page = 1, int size = 10, string search = "");
    Task<ClientResponsibleDto> GetById(int id);
    Task<ClientResponsibleDto> Create(ClientResponsibleDto clientResponsibleDto);
    Task<ClientResponsibleDto> Update(ClientResponsibleDto clientResponsibleDto);
    Task<bool> Delete(int id);
}
