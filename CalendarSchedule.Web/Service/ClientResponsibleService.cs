using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace CalendarSchedule.Web.Service;

public class ClientResponsibleService : IClientResponsibleService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly ITokenStorageService _tokenStorageService;
	private readonly JsonSerializerOptions _serializerOptions;
	private const string? apiEndPoint = "api/ClientResponsible";

	public ClientResponsibleService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
	{
		_httpClientFactory = httpClientFactory;
		_tokenStorageService = tokenStorageService;
		_serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
	}

	public async Task<Result<ClientResponsibleDto>> Create(ClientResponsibleCreateDto clientResponsibleCreateDto)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientResponsibleDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(clientResponsibleCreateDto), Encoding.UTF8, "application/json");

			using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
			if (response.IsSuccessStatusCode)
			{
				var clientResponsible = await response.Content.ReadFromJsonAsync<ClientResponsibleDto>(_serializerOptions);
				if (clientResponsible is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Não econtrado"));

				return Result.Success(clientResponsible);
			}
			else
			{
				var error = await response.Content.ReadFromJsonAsync<Error>(_serializerOptions);
				if (error is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Token não gerado"));
				return Result.Failure<ClientResponsibleDto>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientResponsibleDto>(Error.Internal($"Erro interno: {ex.Message}"));
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
					return Result.Failure<bool>(Error.NotFound("Token não gerado"));

				return Result.Failure<bool>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<bool>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<PagedResultView<ClientResponsibleDto>>> GetAll(int page = 1, int size = 10, string search = "")
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<PagedResultView<ClientResponsibleDto>>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

			if (response.IsSuccessStatusCode)
			{
				var clientResponsibleView = await response.Content.ReadFromJsonAsync<PagedResultView<ClientResponsibleDto>>(_serializerOptions);
				if (clientResponsibleView is null)
					return Result.Failure<PagedResultView<ClientResponsibleDto>>(Error.NotFound("Não econtrado"));
				return Result.Success(clientResponsibleView);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<PagedResultView<ClientResponsibleDto>>(Error.NotFound("Token não gerado"));
				return Result.Failure<PagedResultView<ClientResponsibleDto>>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<PagedResultView<ClientResponsibleDto>>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<ClientResponsibleDto>> GetById(int id)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientResponsibleDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

			if (response.IsSuccessStatusCode)
			{
				var clientResponsibleDto = await response.Content.ReadFromJsonAsync<ClientResponsibleDto>(_serializerOptions);
				if (clientResponsibleDto is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Não econtrado"));

				return Result.Success(clientResponsibleDto);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Token não gerado"));

				return Result.Failure<ClientResponsibleDto>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientResponsibleDto>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}

	public async Task<Result<ClientResponsibleDto>> Update(ClientResponsibleDto clientResponsibleDto)
	{
		try
		{
			var token = await _tokenStorageService.GetToken();
			if (token.IsFailure)
				return Result.Failure<ClientResponsibleDto>(token.Error);

			using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

			StringContent stringContent = new(JsonSerializer.Serialize(clientResponsibleDto), Encoding.UTF8, "application/json");

			using var response = await httpClient.PutAsync($"{apiEndPoint}/{clientResponsibleDto.Id}", stringContent);

			if (response.IsSuccessStatusCode)
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var clientResponsible = await JsonSerializer.DeserializeAsync<ClientResponsibleDto>(resApi, _serializerOptions);
				if (clientResponsible is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Não econtrado"));

				return Result.Success(clientResponsible);
			}
			else
			{
				using Stream resApi = await response.Content.ReadAsStreamAsync();
				var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
				if (error is null)
					return Result.Failure<ClientResponsibleDto>(Error.NotFound("Token não gerado"));
				return Result.Failure<ClientResponsibleDto>(error);
			}
		}
		catch (Exception ex)
		{
			return Result.Failure<ClientResponsibleDto>(Error.Internal($"Erro interno: {ex.Message}"));
		}
	}
}
