using ScheduleRooms.API.MappingDto.ClientContactDtos;
using ScheduleRooms.API.MappingDto.UserContactContatctDto;
using ScheduleRooms.API.Repository;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service
{
    public class ClientResponsibleService(IClientContactRepository clientContactRepository) : IClientResponsibleService
    {
        private readonly IClientContactRepository _clientContactRepository = clientContactRepository;

        public async Task<ClientContactDto> Create(ClientContactDto clientContactDto)
        {
            var clientContact = await _clientContactRepository.Create(clientContactDto.ConvertDtoToClientContact());
            return clientContact.ConvertClientContactToDto();
        }

        public async Task Delete(int id)
        {
            var clientContact = await GetById(id);
            if (clientContact is not null)
            {
                await _clientContactRepository.Delete(clientContact.Id);
            }
        }

        public async Task<IEnumerable<ClientContactDto>> GetAll(int page, int size, string search)
        {
            var clientContacts = await _clientContactRepository.GetAll(page, size, search);
            return clientContacts.ConvertClientContactsToDto();
        }

        public async Task<ClientContactDto> GetById(int id)
        {
            var clientContact = await _clientContactRepository.GetById(id);
            return clientContact.ConvertClientContactToDto();

        }

        public async Task<int> TotalClientResponsible(string search)
        {
            var totalClientContact = await _clientContactRepository.TotalClientContact(search);
            return totalClientContact;
        }

        public async Task<ClientContactDto> Update(ClientContactDto clientContactDto)
        {
            var clientContact = await _clientContactRepository.Update(clientContactDto.ConvertDtoToClientContact());
            return clientContact.ConvertClientContactToDto();
        }
    }
}
