using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class UserService : IUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/User";

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<UserDto> Create(UserDto userDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            StringContent stringContent = new(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");

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
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.DeleteAsync($"{apiEndPoint}/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return new();
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
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
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
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
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

    public async Task<UserDto> Update(UserDto userDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

            StringContent stringContent = new(JsonSerializer.Serialize(userDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{userDto.Id}", stringContent))
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
}
