using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ScheduleRoomService : IScheduleRoomService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/ScheduleRoom";

    public ScheduleRoomService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _tokenStorageService = tokenStorageService;
    }

    public async Task<ScheduleRoomDto> Create(ScheduleRoomDto scheduleDto)
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

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var schedule = await JsonSerializer.DeserializeAsync<ScheduleRoomDto>(resApi, _serializerOptions);
                    if (schedule is not null)
                    {
                        return schedule;
                    }
                }
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    throw new Exception("Informações conflitantes");
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
            return new();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ScheduleRoomView> GetAll(int page = 1, int size = 10, string search = "")
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
            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");


            if (response.IsSuccessStatusCode)
            {
                var scheduleView = await response.Content.ReadFromJsonAsync<ScheduleRoomView>(_serializerOptions);
                return scheduleView ?? new();
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

    public async Task<ScheduleRoomDto> GetById(int id)
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
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleDto = await response.Content.ReadFromJsonAsync<ScheduleRoomDto>(_serializerOptions);
                return scheduleDto ?? new();
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

    public async Task<ScheduleRoomDto> Update(ScheduleRoomDto scheduleDto)
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

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var schedule = await JsonSerializer.DeserializeAsync<ScheduleRoomDto>(resApi, _serializerOptions);
                    if (schedule is not null)
                    {
                        return schedule;
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

    public async Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActive()
    {
        try
        {
            var token = await _tokenStorageService.GetToken();

            if (token.Bearer is null)
            {
                return [];
            }

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActive");


            if (response.IsSuccessStatusCode)
            {
                var scheduleDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleRoomDto>>(_serializerOptions);
                return scheduleDto ??= [];
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return [];
                }
                response.EnsureSuccessStatusCode();
            }
            return [];
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ScheduleRoomDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();

            if (token.Bearer is null)
            {
                return [];
            }

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveRoomId/{roomId}/{dateSalected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");


            if (response.IsSuccessStatusCode)
            {
                var scheduleView = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleRoomDto>>(_serializerOptions);
                return scheduleView ??= [];
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ScheduleRoomDto>();
                }
                response.EnsureSuccessStatusCode();
            }
            return new List<ScheduleRoomDto>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
