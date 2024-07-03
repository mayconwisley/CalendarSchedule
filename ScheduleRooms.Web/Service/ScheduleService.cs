using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ScheduleService : IScheduleService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/Schedule";

    public ScheduleService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenStorageService = tokenStorageService;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ScheduleDto> Create(ScheduleCreateDto scheduleCreateDto)
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
            StringContent stringContent = new(JsonSerializer.Serialize(scheduleCreateDto), Encoding.UTF8, "application/json");

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

    public async Task<ScheduleView> GetAll(int page, int size, string search)
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
                ScheduleView? scheduleView = await response.Content.ReadFromJsonAsync<ScheduleView>(_serializerOptions);
                return scheduleView ??= new();
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

    public async Task<ScheduleDto> GetById(int id)
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
                var scheduleDto = await response.Content.ReadFromJsonAsync<ScheduleDto>(_serializerOptions);
                return scheduleDto ??= new();
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

    public Task<IEnumerable<ScheduleDto>> GetBySchedule()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleDto>> GetByScheduleActive()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
    {
        throw new NotImplementedException();
    }

    public Task<int> TotalSchedules(string search)
    {
        throw new NotImplementedException();
    }

    public async Task<ScheduleDto> Update(ScheduleCreateDto scheduleCreateDto)
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

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleCreateDto), Encoding.UTF8, "application/json");
            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleCreateDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var scheduleDto = await JsonSerializer.DeserializeAsync<ScheduleDto>(resApi, _serializerOptions);
                    if (scheduleDto is not null)
                    {
                        return scheduleDto;
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
