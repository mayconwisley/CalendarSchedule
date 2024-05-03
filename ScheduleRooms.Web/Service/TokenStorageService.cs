using Blazored.SessionStorage;
using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Service.Interface;

namespace ScheduleRooms.Web.Service;

public class TokenStorageService(ISessionStorageService sessionStorageService, ILoginService loginService, IUserService userService) : ITokenStorageService
{
    private const string key = "Token";
    private const string sessionUser = "DadoUser";
    private readonly IUserService _userService = userService;
    private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
    private readonly ILoginService _loginService = loginService;

    public async Task<TokenDto> GetToken()
    {
        var tokenDto = await _sessionStorageService.GetItemAsync<TokenDto>($"{key}");
        if (tokenDto.Bearer is not null)
        {
            return tokenDto;
        }
        return new();
    }
    public async Task<TokenDto> GetToken(LoginDto login)
    {
        return await _sessionStorageService.GetItemAsync<TokenDto>($"{key}") ?? await AddToken(login);
    }
    private async Task<TokenDto> AddToken(LoginDto login)
    {
        var token = await _loginService.Token(login);

        if (token is not null)
        {
            await _sessionStorageService.SetItemAsync(key, token);
            return token;
        }
        return new();
    }
    public async Task RemoverToken()
    {
        await _sessionStorageService.ClearAsync();
    }

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
