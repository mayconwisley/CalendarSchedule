using CalendarSchedule.API.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service.Interface;

public interface IClientResponsibleService
{
    Task<Result<IEnumerable<ClientResponsibleDto>>> GetAll(int page, int size, string search);
    Task<Result<ClientResponsibleDto>> GetById(int id);
    Task<Result<ClientResponsibleDto>> Create(ClientResponsibleCreateDto clientResponsibleCreateDto);
    Task<Result<ClientResponsibleDto>> Update(ClientResponsibleCreateDto clientResponsibleCreateDto);
    Task<Result> Delete(int id);
    Task<Result<int>> TotalClientResponsible(string search);
}
