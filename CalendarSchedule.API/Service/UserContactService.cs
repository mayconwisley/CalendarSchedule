using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.UserContactContatctDto;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class UserContactService(IUserContactRepository _userContactRepository) : IUserContactService
{
    public async Task<Result<UserContactDto>> Create(UserContactCreateDto userContactCreateDto)
    {
        var user = await _userContactRepository.Create(userContactCreateDto.ConvertDtoToUserContactCreate());
        if (user.Id == 0)
            return Result.Failure<UserContactDto>(Error.BadRequest("Erro ao cadastrar contato do usuário"));

        var dto = user.ConvertUserContactToDto();
        return Result.Success(dto);
    }

    public async Task<Result> Delete(int id)
    {
        var userContact = await GetById(id);
        if (userContact.IsFailure)
            return Result.Failure<UserContactDto>(userContact.Error);

        await _userContactRepository.Delete(userContact.Value.Id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<UserContactDto>>> GetAll(int page, int size, string search)
    {
        var userContacts = await _userContactRepository.GetAll(page, size, search);
        if (!userContacts.Any())
            return Result.Failure<IEnumerable<UserContactDto>>(Error.NotFound("Não encontrado"));

        var dto = userContacts.ConvertUserContactsToDto();
        return Result.Success(dto);
    }

    public async Task<Result<UserContactDto>> GetById(int id)
    {
        var userContact = await _userContactRepository.GetById(id);
        if (userContact is null)
            return Result.Failure<UserContactDto>(Error.NotFound("Contato não encontrado"));
        var dto = userContact.ConvertUserContactToDto();
        return Result.Success(dto);
    }

    public async Task<Result<IEnumerable<UserContactDto>>> GetByUserId(int page, int size, int userId)
    {
        var userContacts = await _userContactRepository.GetByUserId(page, size, userId);
        if (!userContacts.Any())
            return Result.Failure<IEnumerable<UserContactDto>>(Error.NotFound("Não encontrado"));

        var dto = userContacts.ConvertUserContactsToDto();
        return Result.Success(dto);
    }

    public async Task<Result<int>> TotalUserContact(string search)
    {
        var totalUserContact = await _userContactRepository.TotalUserContact(search);
        if (totalUserContact <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum contato encontrado"));

        return Result.Success(totalUserContact);
    }

    public async Task<Result<UserContactDto>> Update(UserContactCreateDto userContactCreateDto)
    {
        var userContact = await _userContactRepository.Update(userContactCreateDto.ConvertDtoToUserContactCreate());
        if (userContact is null)
            return Result.Failure<UserContactDto>(Error.NotFound("Contato não encontrado"));

        var dto = userContact.ConvertUserContactToDto();
        return Result.Success(dto);
    }
}
