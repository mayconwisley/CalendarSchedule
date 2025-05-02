using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/Client";

    public ClientService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _tokenStorageService = tokenStorageService;
    }

    public async Task<Result<ClientDto>> Create(ClientDto clientDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ClientDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);


            StringContent stringContent = new(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
            if ((response.IsSuccessStatusCode))
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();

                var client = await JsonSerializer.DeserializeAsync<ClientDto>(resApi, _serializerOptions);
                if (client is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Não econtrado"));
                return Result.Success(client);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Token não gerado"));
                return Result.Failure<ClientDto>(error);
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
                    return Result.Failure<bool>(Error.NotFound("Cliente Contact Nulo"));
                return Result.Failure<bool>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<PagedResultView<ClientDto>>> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<ClientDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);


            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                var clientView = await response.Content.ReadFromJsonAsync<PagedResultView<ClientDto>>(_serializerOptions);
                if (clientView is null)
                    return Result.Failure<PagedResultView<ClientDto>>(Error.NotFound("Não econtrado"));
                return Result.Success(clientView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ClientDto>>(Error.NotFound("Token não gerado"));
                return Result.Failure<PagedResultView<ClientDto>>(error);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<Result<ClientDto>> GetById(int id)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ClientDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var clientDto = await response.Content.ReadFromJsonAsync<ClientDto>(_serializerOptions);
                if (clientDto is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Não econtrado"));
                return Result.Success(clientDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Token não gerado"));
                return Result.Failure<ClientDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<ClientDto>> Update(ClientDto clientDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ClientDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);


            StringContent stringContent = new(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PutAsync($"{apiEndPoint}/{clientDto.Id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var client = await JsonSerializer.DeserializeAsync<ClientDto>(resApi, _serializerOptions);
                if (client is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Não econtrado"));
                return Result.Success(client);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ClientDto>(Error.NotFound("Token não gerado"));
                return Result.Failure<ClientDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
