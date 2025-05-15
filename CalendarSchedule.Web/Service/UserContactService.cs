using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CalendarSchedule.Web.Service;

public class UserContactService : IUserContactService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ITokenStorageService _tokenStorageService;
	private readonly JsonSerializerOptions _serializerOptions;

	private const string apiEndPoint = "api/UserContact";

	public UserContactService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
	{
		_httpClientFactory = httpClientFactory;
		_tokenStorageService = tokenStorageService;
		_serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

	public async Task<Result<UserContactDto>> Create(UserContactCreateDto userContactCreateDto)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<UserContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(userContactCreateDto), Encoding.UTF8, "application/json");

			using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
			if (response.IsSuccessStatusCode)
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var user = await JsonSerializer.DeserializeAsync<UserContactDto>(resApi, _serializerOptions);
				if (user is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Success(user);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<UserContactDto>(error);
			}

		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Result<bool>> Delete(int id)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<bool>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.DeleteAsync($"{apiEndPoint}/{id}");
			if (response.IsSuccessStatusCode)
			{
				return Result.Success(true);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<bool>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<bool>(error);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Result<PagedResultView<UserDto>>> GetAll(int page = 1, int size = 10, string search = "")
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<PagedResultView<UserDto>>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			// httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

			if (response.IsSuccessStatusCode)
			{
				var userView = await response.Content.ReadFromJsonAsync<PagedResultView<UserDto>>(_serializerOptions);
				if (userView is null)
					return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("User Contact Nulo"));
				return Result.Success(userView);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<PagedResultView<UserDto>>(error);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Result<UserContactDto>> GetById(int id)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<UserContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var userDto = await response.Content.ReadFromJsonAsync<UserContactDto>(_serializerOptions);
				if (userDto is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Success(userDto);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<UserContactDto>(error);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Result<UserContactDto>> Update(UserContactDto userContactDto)
	{
		try
		{
			var userContactUpdateDto = new UserContactUpdateDto
			{
				Id = userContactDto.Id,
				UserId = userContactDto.UserDto.Id,
				Number = userContactDto.Number,
				Type = userContactDto.Type
			};
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<UserContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(userContactUpdateDto), Encoding.UTF8, "application/json");
			using var response = await httpClient.PutAsync($"{apiEndPoint}/{userContactUpdateDto.Id}", stringContent);
			if (response.IsSuccessStatusCode)
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var user = await JsonSerializer.DeserializeAsync<UserContactDto>(resApi, _serializerOptions);
				if (user is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Success(user);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<UserContactDto>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<UserContactDto>(error);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Result<PagedResultView<UserContactDto>>> GetByUserId(int page, int size, int userId)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<PagedResultView<UserContactDto>>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			// httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.GetAsync($"{apiEndPoint}/UserContactByUserId/{userId}");

			if (response.IsSuccessStatusCode)
			{
				var userDtos = await response.Content.ReadFromJsonAsync<PagedResultView<UserContactDto>>(_serializerOptions);
				if (userDtos is null)
					return Result.Failure<PagedResultView<UserContactDto>>(Error.NotFound("User Contact Nulo"));
				return Result.Success(userDtos);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<PagedResultView<UserContactDto>>(Error.NotFound("User Contact Nulo"));
				return Result.Failure<PagedResultView<UserContactDto>>(error);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}
