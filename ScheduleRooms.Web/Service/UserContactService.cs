using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

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

    public async Task<UserDto> Create(UserContactCreateDto userContactCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.Bearer is null)
            {
                return new();
            }

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(userContactCreateDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var user = await JsonSerializer.DeserializeAsync<UserDto>(resApi, _serializerOptions);

                    if (user is not null)
                    {
                        return user;
                    }
                }
            }
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.Bearer is null)
            {
                return new();
            }

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);

            using var response = await httpClient.DeleteAsync($"{apiEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<UserView> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return new();
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                UserView? userView = await response.Content.ReadFromJsonAsync<UserView>(_serializerOptions);
                return userView ??= new();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new();
                }
                response.EnsureSuccessStatusCode();
                return new();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<UserDto> GetById(int id)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return new();
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var userDto = await response.Content.ReadFromJsonAsync<UserDto>(_serializerOptions);
                return userDto ??= new();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new();
                }
                response.EnsureSuccessStatusCode();
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<UserDto> Update(UserContactCreateDto userContactCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();

            if (token.Bearer is null)
            {
                return new();
            }

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(userContactCreateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{userContactCreateDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var user = await JsonSerializer.DeserializeAsync<UserDto>(resApi, _serializerOptions);
                    if (user is not null)
                    {
                        return user;
                    }
                }
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<UserContactView> GetByUserId(int page, int size, int userId)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return new();
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/UserContactByUserId/{userId}?page={page}&size={size}");

            if (response.IsSuccessStatusCode)
            {
                var userDtos = await response.Content.ReadFromJsonAsync<UserContactView>(_serializerOptions);
                return userDtos ??= new();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new();
                }
                response.EnsureSuccessStatusCode();
                return new();
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
