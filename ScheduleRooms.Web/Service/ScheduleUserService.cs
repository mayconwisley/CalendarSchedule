using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ScheduleUserService : IScheduleUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string apiEndPoint = "api/ScheduleUser";

    public ScheduleUserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ScheduleUserDto> Create(ScheduleUserDto scheduleUserDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var scheduleUser = await JsonSerializer.DeserializeAsync<ScheduleUserDto>(resApi, _serializerOptions);
                    if (scheduleUser is not null)
                    {
                        return scheduleUser;
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

    public async Task<ScheduleUserView> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                ScheduleUserView? scheduleUserView = await response.Content.ReadFromJsonAsync<ScheduleUserView>(_serializerOptions);
                return scheduleUserView ??= new();

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

    public async Task<ScheduleUserDto> GetById(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<ScheduleUserDto>(_serializerOptions);
                return scheduleUserDto ??= new();
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

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActive()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActive");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleUserDto>>(_serializerOptions);
                return scheduleUserDto ??= [];

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
                return [];
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveClientId(int clientId, DateTime dateSelected)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveClientId/{clientId}/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleUserDto>>(_serializerOptions);
                return scheduleUserDto ??= [];

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
                return [];
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveClinetIdUserId(int clientId, int userId, DateTime dateSelected)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveClientIdUserId/{clientId}/{userId}/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleUserDto>>(_serializerOptions);
                return scheduleUserDto ??= [];

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
                return [];
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserActiveUserId(int userId, DateTime dateSelected)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveUserId/{userId}/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleUserDto>>(_serializerOptions);
                return scheduleUserDto ??= [];

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
                return [];
            }

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ScheduleUserDto> Update(ScheduleUserDto scheduleUserDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleUserDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var scheduleUser = await JsonSerializer.DeserializeAsync<ScheduleUserDto>(resApi, _serializerOptions);
                    if (scheduleUser is not null)
                    {
                        return scheduleUser;
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

    public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserId(int userId)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleUserDto>>(_serializerOptions);
                return scheduleUserDto ??= [];

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
                return [];
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}
