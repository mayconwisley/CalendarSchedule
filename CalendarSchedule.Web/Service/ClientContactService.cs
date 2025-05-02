using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CalendarSchedule.Web.Service;

public class ClientContactService : IClientContactService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ITokenStorageService _tokenStorageService;
	private readonly JsonSerializerOptions _serializerOptions;

	private const string apiEndPoint = "api/ClientContact";

	public ClientContactService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
	{
		_httpClientFactory = httpClientFactory;
		_tokenStorageService = tokenStorageService;
		_serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

	public async Task<Result<ClientContactDto>> Create(ClientContactCreateDto clientContactCreateDto)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(clientContactCreateDto), Encoding.UTF8, "application/json");

			using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
			if (response.IsSuccessStatusCode)
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var clientContact = await JsonSerializer.DeserializeAsync<ClientContactDto>(resApi, _serializerOptions);

				if (clientContact is null)
					return Result.Failure<ClientContactDto>(Error.NotFound("Cliente Contact Nulo"));

				return Result.Success(clientContact);

			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<ClientContactDto>(Error.NotFound("Cliente Contact Nulo"));
				return Result.Failure<ClientContactDto>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientContactDto>(Error.Internal($"Erro interno: {ex.Message}"));
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
				return Result.Success(true);

			return Result.Failure<bool>(Error.BadRequest("false"));
		}
		catch (Exception ex)
		{
			return Result.Failure<bool>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<PagedResultView<ClientContactDto>>> GetAll(int page = 1, int size = 10, string search = "")
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<PagedResultView<ClientContactDto>>(token.Error);


			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
			using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

			if (response.IsSuccessStatusCode)
			{
				var clientContactView = await response.Content.ReadFromJsonAsync<PagedResultView<ClientContactDto>>(_serializerOptions);
				if (clientContactView is null)
					return Result.Failure<PagedResultView<ClientContactDto>>(Error.NotFound("Cliente Contact Nulo"));

				return Result.Success(clientContactView);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<PagedResultView<ClientContactDto>>(Error.NotFound("Token não gerado"));

				return Result.Failure<PagedResultView<ClientContactDto>>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<PagedResultView<ClientContactDto>>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<ClientContactDto>> GetById(int id)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
			using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var clientContactDto = await response.Content.ReadFromJsonAsync<ClientContactDto>(_serializerOptions);
				if (clientContactDto is null)
					return Result.Failure<ClientContactDto>(Error.NotFound("Cliente Contact Nulo"));

				return Result.Success(clientContactDto);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<ClientContactDto>(Error.NotFound("Token não gerado"));
				return Result.Failure<ClientContactDto>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientContactDto>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<PagedResultView<ClientContactDto>>> GetByClientId(int page = 1, int size = 10, int userId = 0)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<PagedResultView<ClientContactDto>>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
			using var response = await httpClient.GetAsync($"{apiEndPoint}/ContactByClientId/{userId}");

			if (response.IsSuccessStatusCode)
			{
				var clientContactDtos = await response.Content.ReadFromJsonAsync<PagedResultView<ClientContactDto>>(_serializerOptions);
				if (clientContactDtos is null)
					return Result.Failure<PagedResultView<ClientContactDto>>(Error.NotFound("Cliente Contact Nulo"));

				return Result.Success(clientContactDtos);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<PagedResultView<ClientContactDto>>(Error.NotFound("Token não gerado"));
				return Result.Failure<PagedResultView<ClientContactDto>>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<PagedResultView<ClientContactDto>>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<ClientContactDto>> Update(ClientContactUpdateDto clientContactUpdateDto)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientContactDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(clientContactUpdateDto), Encoding.UTF8, "application/json");
			using (var response = await httpClient.PutAsync($"{apiEndPoint}/{clientContactUpdateDto.Id}", stringContent))
			{
				if (response.IsSuccessStatusCode)
				{
					using Stream resApi = await response.Content.ReadAsStreamAsync();
					var clientContactDto = await JsonSerializer.DeserializeAsync<ClientContactDto>(resApi, _serializerOptions);
					if (clientContactDto is null)
						return Result.Failure<ClientContactDto>(Error.NotFound("Cliente Contact Nulo"));

					return Result.Success(clientContactDto);
				}
				else
				{
					using Stream resApi = await response.Content.ReadAsStreamAsync();
					var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
					if (error is null)
						return Result.Failure<ClientContactDto>(Error.NotFound("Token não gerado"));
					return Result.Failure<ClientContactDto>(error);
				}
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientContactDto>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}
}
