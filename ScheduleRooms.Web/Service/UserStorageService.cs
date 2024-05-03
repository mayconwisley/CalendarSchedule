using Blazored.SessionStorage;
using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Service.Interface;

namespace ScheduleRooms.Web.Service;

public class UserStorageService(ISessionStorageService sessionStorageService, IUserService userService) : IUserStorageService
{
    private const string sessionUser = "DadoUser";
    private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
    private readonly IUserService _userService = userService;

    public async Task<UserDto> GetUserSession()
    {
        var userDto = await _sessionStorageService.GetItemAsync<UserDto>($"{sessionUser}");
        if (userDto is not null)
        {
            return userDto;
        }
        return new();
    }

    public async Task<UserDto> GetUserSession(LoginDto loginDto)
    {
        return await _sessionStorageService.GetItemAsync<UserDto>($"{sessionUser}") ?? await AddUserSession(loginDto);
    }
    private async Task<UserDto> AddUserSession(LoginDto loginDto)
    {
        var user = await _userService.GetManagerUsername(loginDto.Username);
        if (user is not null)
        {
            await _sessionStorageService.SetItemAsync(sessionUser, user);
            return user;
        }
        return new();
    }
    public async Task RemoveUserSession()
    {
        await _sessionStorageService.ClearAsync();
    }
}
