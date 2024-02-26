using ScheduleRooms.API.MappingDto.UserDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task Create(UserDto userDto)
    {
        await _userRepository.Create(userDto.ConvertDtoToUser());
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
        var userEntity = await _userRepository.GetById(id);
        return userEntity.ConvertUserToDto();
    }

    public async Task<int> TotalUsers(string search)
    {
        var totalUser = await _userRepository.TotalUser(search);
        return totalUser;
    }

    public async Task Update(UserDto userDto)
    {
        await _userRepository.Update(userDto.ConvertDtoToUser());
    }
}
