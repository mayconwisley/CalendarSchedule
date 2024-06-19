using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface
{
    public interface IClientResponsibleService
    {
        Task<IEnumerable<ClientResponsibleDto>> GetAll(int page, int size, string search);
        Task<ClientResponsibleDto> GetById(int id);
        Task<ClientResponsibleDto> Create(ClientResponsibleCreateDto clientResponsibleCreateDto);
        Task<ClientResponsibleDto> Update(ClientResponsibleCreateDto clientResponsibleCreateDto);
        Task Delete(int id);
        Task<int> TotalClientResponsible(string search);
    }
}
