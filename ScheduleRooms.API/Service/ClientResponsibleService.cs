using ScheduleRooms.API.MappingDto.ClientResponsibleDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service
{
    public class ClientResponsibleService(IClientResponsibleRepository clientResponsibleRepository) : IClientResponsibleService
    {
        private readonly IClientResponsibleRepository _clientResponsibleRepository = clientResponsibleRepository;

        public async Task<ClientResponsibleDto> Create(ClientResponsibleCreateDto clientResponsibleCreateDto)
        {
            var clientResponsible = await _clientResponsibleRepository.Create(clientResponsibleCreateDto.ConvertDtoToClientResponsibleCreate());
            return clientResponsible.ConvertClientResponsibleToDto();
        }

        public async Task Delete(int id)
        {
            var clientResponsible = await GetById(id);
            if (clientResponsible is not null)
            {
                await _clientResponsibleRepository.Delete(clientResponsible.Id);
            }
        }

        public async Task<IEnumerable<ClientResponsibleDto>> GetAll(int page, int size, string search)
        {
            var clientResponsibles = await _clientResponsibleRepository.GetAll(page, size, search);
            return clientResponsibles.ConvertClientResponsibleToDtos();
        }

        public async Task<ClientResponsibleDto> GetById(int id)
        {
            var clientResponsible = await _clientResponsibleRepository.GetById(id);
            return clientResponsible.ConvertClientResponsibleToDto();

        }

        public async Task<int> TotalClientResponsible(string search)
        {
            var totalClientContact = await _clientResponsibleRepository.TotalClientResponsible(search);
            return totalClientContact;
        }

        public async Task<ClientResponsibleDto> Update(ClientResponsibleCreateDto clientResponsibleCreateDto)
        {
            var clientResponsible = await _clientResponsibleRepository.Update(clientResponsibleCreateDto.ConvertDtoToClientResponsibleCreate());
            return clientResponsible.ConvertClientResponsibleToDto();
        }
    }
}
