using Blazored.SessionStorage;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

public class TokenStorageService(ISessionStorageService sessionStorageService, ILoginService loginService) : ITokenStorageService
{
	private const string key = "Token";

	private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
	private readonly ILoginService _loginService = loginService;

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
			await AddToken(login);
			return Result.Failure<TokenDto>(Error.NullValue("Token Nullo"));
		}

		return Result.Success(tokenDto);
	}
	private async Task<TokenDto> AddToken(LoginDto login)
	{
		var token = await _loginService.Token(login);

		if (token.IsSuccess)
		{
			await _sessionStorageService.SetItemAsync(key, token);
			return token.Value;
		}
		return new();
	}
	public async Task RemoverToken()
	{
		await _sessionStorageService.ClearAsync();
	}
}
