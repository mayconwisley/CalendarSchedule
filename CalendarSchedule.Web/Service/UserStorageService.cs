using Blazored.SessionStorage;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

public class UserStorageService(ISessionStorageService sessionStorageService, IUserService userService) : IUserStorageService
{
	private const string sessionUser = "DadoUser";
	private readonly ISessionStorageService _sessionStorageService = sessionStorageService;
	private readonly IUserService _userService = userService;

	public async Task<Result<UserSessionDto>> GetUserSession()
	{
		var user = await _sessionStorageService.GetItemAsync<UserSessionDto>($"{sessionUser}");
		if (user is null)
			return Result.Failure<UserSessionDto>(Error.NotFound("Sem tokem na sessão"));

		return Result.Success(user);
	}

	public async Task<Result<UserSessionDto>> GetUserSession(LoginDto loginDto)
	{
		var user = await _sessionStorageService.GetItemAsync<UserSessionDto>($"{sessionUser}");
		if (user is null)
		{
			await AddUserSession(loginDto);
			return Result.Failure<UserSessionDto>(Error.NullValue("Adicionando token"));
		}

		return Result.Success(user);
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
