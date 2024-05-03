using Blazored.SessionStorage;
using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Service.Interface;

namespace ScheduleRooms.Web.Service;

public class UserStorageService(ISessionStorageService sessionStorageService, IUserService userService) : IUserStorageService
{
    private const string sessionUser = "DadoUser";
    private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
    private readonly IUserService _userService = userService;

    public async Task<UserSessionDto> GetUserSession()
    {
        var user = await _sessionStorageService.GetItemAsync<UserSessionDto>($"{sessionUser}");
        if (user is not null)
        {
            return user;
        }
        return new();
    }

    public async Task<UserSessionDto> GetUserSession(LoginDto loginDto)
    {
        var user = await _sessionStorageService.GetItemAsync<UserSessionDto>($"{sessionUser}") ?? await AddUserSession(loginDto);
        return user;
    }
    private async Task<UserSessionDto> AddUserSession(LoginDto loginDto)
    {
        var user = await _userService.GetManagerUsername(loginDto.Username);
        UserSessionDto userSession = new()
        {
            Username = user.Username,
            Manager = user.Manager
        };

        if (user is not null)
        {
            await _sessionStorageService.SetItemAsync(sessionUser, userSession);
            return userSession;
        }
        return new();
    }
    public async Task RemoveUserSession()
    {
        await _sessionStorageService.ClearAsync();
    }
}
