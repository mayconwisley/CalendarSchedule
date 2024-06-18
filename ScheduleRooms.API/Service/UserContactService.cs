using ScheduleRooms.API.MappingDto.UserContactContatctDto;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class UserContactService(IUserContactRepository userContactRepository) : IUserContactService
{
    IUserContactRepository _userContactRepository = userContactRepository;

    public async Task<UserContactDto> Create(UserContactCreateDto userContactCreateDto)
    {
        var user = await _userContactRepository.Create(userContactCreateDto.ConvertDtoToUserContactCreate());
        return user.ConvertUserContactToDto();
    }

    public async Task Delete(int id)
    {
        var userContact = await GetById(id);
        if (userContact is not null)
        {
            await _userContactRepository.Delete(userContact.Id);
        }
    }

    public async Task<IEnumerable<UserContactDto>> GetAll(int page, int size, string search)
    {
        var userContacts = await _userContactRepository.GetAll(page, size, search);
        return userContacts.ConvertUserContactsToDto();
    }

    public async Task<UserContactDto> GetById(int id)
    {
        var userContact = await _userContactRepository.GetById(id);
        return userContact.ConvertUserContactToDto();
    }

    public async Task<IEnumerable<UserContactDto>> GetByUserId(int page, int size, int userId)
    {
        var userContacts = await _userContactRepository.GetByUserId(page, size, userId);
        return userContacts.ConvertUserContactsToDto();
    }

    public async Task<int> TotalUserContact(string search)
    {
        var totalUserContact = await _userContactRepository.TotalUserContact(search);
        return totalUserContact;
    }

    public async Task<UserContactDto> Update(UserContactCreateDto userContactCreateDto)
    {
        var userContact = await _userContactRepository.Update(userContactCreateDto.ConvertDtoToUserContactCreate());
        return userContact.ConvertUserContactToDto();
    }
}
