using ScheduleRooms.API.MappingDto.LoginDtos;
using ScheduleRooms.API.MappingDto.UserDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.API.Utility.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class UserService(IUserRepository userRepository,
                         IEncryptionUtility encryptionUtility,
                         IDecryptionUtility decryptionUtility) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IEncryptionUtility _encryptionUtility = encryptionUtility;
    private readonly IDecryptionUtility _decryptionUtility = decryptionUtility;

    public async Task Create(UserDto userDto)
    {
        UserDto user = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Cellphone = userDto.Cellphone,
            Username = userDto.Username,
            Password = _encryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };

        await _userRepository.Create(user.ConvertDtoToUser());
    }

    public async Task Delete(int id)
    {
        var userEntity = await GetById(id);
        if (userEntity is not null)
        {
            await _userRepository.Delete(userEntity.Id);
        }
    }

    public async Task<IEnumerable<UserDto>> GetAll(int page, int size, string search)
    {
        var userEntity = await _userRepository.GetAll(page, size, search);
        return userEntity.ConvertUsersToDto();
    }

    public async Task<UserDto> GetById(int id)
    {
        var userDto = await _userRepository.GetById(id);
        UserDto user = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Cellphone = userDto.Cellphone,
            Username = userDto.Username,
            Password = _decryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };
        return user;
    }

    public async Task<IEnumerable<UserDto>> GetManagerAll(int page, int size, string search)
    {
        var userEntity = await _userRepository.GetManagerAll(page, size, search);
        return userEntity.ConvertUsersToDto();
    }

    public async Task<UserDto> GetManagerUsername(string username)
    {
        var userEntity = await _userRepository.GetManagerUsername(username);
        return userEntity.ConvertUserToDto();
    }

    public async Task<bool> GetPassword(LoginDto login)
    {
        var pass = await _userRepository.GetPassword(login.ConvertLoginDtoToLoginApi());
        pass = _decryptionUtility.Dado(pass);

        if (pass == login.Password)
        {
            return true;
        }
        return false;
    }

    public async Task<int> TotalUsers(string search)
    {
        var totalUser = await _userRepository.TotalUser(search);
        return totalUser;
    }

    public async Task Update(UserDto userDto)
    {
        UserDto user = new()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Description = userDto.Description,
            Cellphone = userDto.Cellphone,
            Username = userDto.Username,
            Password = _encryptionUtility.Dado(userDto.Password),
            Manager = userDto.Manager,
            Active = userDto.Active
        };

        await _userRepository.Update(user.ConvertDtoToUser());
    }
}
