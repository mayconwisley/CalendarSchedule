using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/User";

    public UserService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _tokenStorageService = tokenStorageService;
    }

    public async Task<Result<UserDto>> Create(UserCreateDto userCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<UserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(userCreateDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var user = await JsonSerializer.DeserializeAsync<UserDto>(resApi, _serializerOptions);
                if (user is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Success(user);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<UserDto>(error);
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
                    return Result.Failure<bool>(Error.NotFound("Usuário Nulo"));
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
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                var userView = await response.Content.ReadFromJsonAsync<PagedResultView<UserDto>>(_serializerOptions);
                if (userView is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Success(userView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<PagedResultView<UserDto>>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<PagedResultView<UserDto>>> GetManagerAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<UserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/ManagerAll?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                var userView = await response.Content.ReadFromJsonAsync<PagedResultView<UserDto>>(_serializerOptions);
                if (userView is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Success(userView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<PagedResultView<UserDto>>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<UserDto>> GetById(int id)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<UserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<UserDto>(_serializerOptions);
                if (userDto is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Success(userDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<UserDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<UserDto>> Update(UserUpdateDto userUpdateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<UserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(userUpdateDto), Encoding.UTF8, "application/json");
            using var response = await httpClient.PutAsync($"{apiEndPoint}/{userUpdateDto.Id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var user = await JsonSerializer.DeserializeAsync<UserDto>(resApi, _serializerOptions);
                if (user is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Success(user);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<UserDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<UserDto>> GetManagerUsername(string username)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<UserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/Username/{username}");

            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<UserDto>(_serializerOptions);
                if (userDto is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Success(userDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<UserDto>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<UserDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<PagedResultView<UserDto>>> GetManagerAllByUserCurrent(int page = 1, int size = 10, string search = "", string username = "")
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<UserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/ManagerAllByUserCurrent?page={page}&size={size}&search={search}&username={username}");

            if (response.IsSuccessStatusCode)
            {
                var userView = await response.Content.ReadFromJsonAsync<PagedResultView<UserDto>>(_serializerOptions);
                if (userView is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Success(userView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<UserDto>>(Error.NotFound("Usuário Nulo"));
                return Result.Failure<PagedResultView<UserDto>>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
