using Blazored.SessionStorage;
using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Service.Interface;

namespace ScheduleRooms.Web.Service;

public class TokenStorageService(ISessionStorageService sessionStorageService, ILoginService loginService) : ITokenStorageService
{
    private const string key = "Token";
   
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
}
