using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service.Interface
{
    public interface IClientResponsibleService
    {
        Task<IEnumerable<ClientContactDto>> GetAll(int page, int size, string search);
        Task<ClientContactDto> GetById(int id);
        Task<ClientContactDto> Create(ClientContactDto clientContactDto);
        Task<ClientContactDto> Update(ClientContactDto clientContactDto);
        Task Delete(int id);
        Task<int> TotalClientResponsible(string search);
    }
}
