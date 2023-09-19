using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Drawing;

namespace ScheduleRooms.Web.Service;

public class ScheduleService : IScheduleService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/Schedule";

    public ScheduleService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ScheduleDto> Create(ScheduleDto scheduleDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var schedule = await JsonSerializer.DeserializeAsync<ScheduleDto>(resApi, _serializerOptions);
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

    public async Task<ScheduleView> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");


            if (response.IsSuccessStatusCode)
            {
                var scheduleView = await response.Content.ReadFromJsonAsync<ScheduleView>(_serializerOptions);
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

    public async Task<ScheduleDto> GetById(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleDto = await response.Content.ReadFromJsonAsync<ScheduleDto>(_serializerOptions);
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

    public async Task<ScheduleDto> Update(ScheduleDto scheduleDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var schedule = await JsonSerializer.DeserializeAsync<ScheduleDto>(resApi, _serializerOptions);
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

    public async Task<IEnumerable<ScheduleDto>> GetByAgendaActive()
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActive");


            if (response.IsSuccessStatusCode)
            {
                var scheduleDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
                return scheduleDto ??= new List<ScheduleDto>();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ScheduleDto>();
                }
                response.EnsureSuccessStatusCode();
            }
            return new List<ScheduleDto>();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<ScheduleDto>> GetByAgendaActiveSalaId(int roomId, DateTime dateSalected)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveRoomId/{roomId}/{dateSalected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");


            if (response.IsSuccessStatusCode)
            {
                var scheduleView = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
                return scheduleView ??= new List<ScheduleDto>();
            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return new List<ScheduleDto>();
                }
                response.EnsureSuccessStatusCode();
            }
            return new List<ScheduleDto>();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
