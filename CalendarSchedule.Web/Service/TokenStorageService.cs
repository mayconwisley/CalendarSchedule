using Blazored.SessionStorage;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

public class TokenStorageService(ISessionStorageService _sessionStorageService, ILoginService _loginService) : ITokenStorageService
{
    private const string key = "Token";

    public async Task<Result<TokenDto>> GetToken()
    {
        var tokenDto = await _sessionStorageService.GetItemAsync<TokenDto>($"{key}");
        if (tokenDto is null)
            return Result.Failure<TokenDto>(Error.NullValue("Token Nullo"));

        return Result.Success(tokenDto);
    }
    public async Task<Result<TokenDto>> GetToken(LoginDto login)
    {
        var tokenDto = await _sessionStorageService.GetItemAsync<TokenDto>($"{key}");
        if (tokenDto is null)
        {
            var result = await AddToken(login);
            if (result.IsSuccess)
            {
                return Result.Success(result.Value);
            }
            return Result.Failure<TokenDto>(result.Error);
        }
        return Result.Success(tokenDto);

    }
    private async Task<Result<TokenDto>> AddToken(LoginDto login)
    {
        var token = await _loginService.Token(login);

        if (token.IsSuccess)
        {
            await _sessionStorageService.SetItemAsync(key, token.Value);
            return Result.Success(token.Value);
        }
        return Result.Failure<TokenDto>(Error.Unauthorized("Usuário ou Senha inválido"));
    }
    public async Task RemoverToken()
    {
        await _sessionStorageService.ClearAsync();
    }
}
