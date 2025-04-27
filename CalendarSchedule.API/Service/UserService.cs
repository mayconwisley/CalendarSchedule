using CalendarSchedule.API.Abstractions;
using CalendarSchedule.API.MappingDto.LoginDtos;
using CalendarSchedule.API.MappingDto.UserDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.API.Utility.Interface;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class UserService(IUserRepository _userRepository,
                         IEncryptionUtility _encryptionUtility,
                         IDecryptionUtility _decryptionUtility) : IUserService
{
    public async Task<Result<UserDto>> Create(UserDto userDto)
    {
        UserDto userDt = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Username = userDto.Username,
            Password = _encryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };

        var user = await _userRepository.Create(userDt.ConvertDtoToUser());
        if (user.Id == 0)
            return Result.Failure<UserDto>(Error.Internal("Erro ao criar usuário"));

        var dto = user.ConvertUserToDto();

        return Result.Success(dto);
    }

    public async Task<Result> Delete(int id)
    {
        var userEntity = await GetById(id);
        if (userEntity.IsFailure)
            return Result.Failure<UserDto>(userEntity.Error);

        await _userRepository.Delete(userEntity.Value.Id);
        return Result.Success();
    }

    public async Task<Result<IEnumerable<UserDto>>> GetAll(int page, int size, string search)
    {
        var userEntity = await _userRepository.GetAll(page, size, search);
        if (userEntity is null)
            return Result.Failure<IEnumerable<UserDto>>(Error.NotFound("Nenhum usuário encontrado"));

        var dto = userEntity.ConvertUsersToDto();
        return Result.Success(dto);
    }

    public async Task<Result<UserDto>> GetById(int id)
    {
        var userDto = await _userRepository.GetById(id);
        if (userDto is null)
            return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

        UserDto user = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Username = userDto.Username,
            Password = _decryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };

        return Result.Success(user);
    }

    public async Task<Result<IEnumerable<UserDto>>> GetManagerAll(int page, int size, string search)
    {
        var userEntity = await _userRepository.GetManagerAll(page, size, search);
        if (userEntity is null)
            return Result.Failure<IEnumerable<UserDto>>(Error.NotFound("Nenhum usuário encontrado"));
        var dto = userEntity.ConvertUsersToDto();
        return Result.Success(dto);
    }

    public async Task<Result<IEnumerable<UserDto>>> GetManagerAllByUserCurrent(int page, int size, string search, string username)
    {
        var users = await _userRepository.GetManagerAllByUserCurrent(page, size, search, username);
        if (users is null)
            return Result.Failure<IEnumerable<UserDto>>(Error.NotFound("Nenhum usuário encontrado"));

        var dto = users.ConvertUsersToDto();
        return Result.Success(dto);
    }

    public async Task<Result<UserDto>> GetManagerUsername(string username)
    {
        var userEntity = await _userRepository.GetManagerUsername(username);
        if (userEntity is null)
            return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

        var dto = userEntity.ConvertUserToDto();
        return Result.Success(dto);
    }

    public async Task<Result<bool>> IsPassword(LoginDto login)
    {
        var password = await _userRepository.GetPassword(login.ConvertLoginDtoToLoginApi());
        if (string.IsNullOrEmpty(password))
            return Result.Failure<bool>(Error.Validation("Senha inválida"));

        password = _decryptionUtility.Dado(password);

        if (password == login.Password)
        {
            return Result.Success(true);
        }
        return Result.Failure<bool>(Error.Validation("Erro ao validar senha"));
    }

    public async Task<Result<bool>> IsUsername(string username)
    {
        var isUsername = await _userRepository.IsUsername(username);
        if (isUsername)
            return Result.Success(true);

        return Result.Failure<bool>(Error.NotFound("Usuário não encontrado"));
    }

    public async Task<Result<int>> TotalUsers(string search)
    {
        var totalUser = await _userRepository.TotalUser(search);
        if (totalUser <= 0)
            return Result.Failure<int>(Error.NotFound("Nenhum usuário encontrado"));

        return Result.Success(totalUser);
    }

    public async Task<Result<UserDto>> Update(UserDto userDto)
    {
        UserDto user = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Username = userDto.Username,
            Password = _encryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };

        var result = await _userRepository.Update(user.ConvertDtoToUser());
        if (result is null)
            return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));
        var dto = result.ConvertUserToDto();
        return Result.Success(dto);
    }
}
