using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ScheduleUserService : IScheduleUserService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string apiEndPoint = "api/ScheduleUser";

    public ScheduleUserService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        _tokenStorageService = tokenStorageService;
    }

    public async Task<ScheduleUserDto> Create(ScheduleUserCreateDto scheduleUserCreateDto)
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

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserCreateDto), Encoding.UTF8, "application/json");

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
                else
                {
                    throw new Exception(response.EnsureSuccessStatusCode().Content.ToString());
                }
            }
            return new();
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<bool> Delete(int scheduleId, int userId)
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
            using var response = await httpClient.DeleteAsync($"{apiEndPoint}/{scheduleId}/{userId}");
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
    public async Task<ScheduleUserDto> GetById(int scheduleId, int userId)
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
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{scheduleId}/{userId}");

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
    public async Task<IEnumerable<ScheduleDto>> GetByScheduleUserActive()
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActive");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
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
    public async Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClientId(int clientId, DateTime dateSelected)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveClientId/{clientId}/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
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
    public async Task<IEnumerable<ScheduleDto>> GetByScheduleUserActiveClinetIdUserId(int clientId, int userId, DateTime dateSelected)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleActiveClientIdUserId/{clientId}/{userId}/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
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
    public async Task<ScheduleUserView> GetByScheduleUserDateStart(DateTime dateSelected)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/DateStart/{dateSelected.ToString("dd/MM/yyyy").Replace("/", "%2F")}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<ScheduleUserView>(_serializerOptions);
                return scheduleUserDto ??= new();

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
    public async Task<ScheduleUserDto> Update(ScheduleUserCreateDto scheduleUserCreateDto)
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
            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserCreateDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleUserCreateDto.ScheduleId}", stringContent))
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
    public async Task<IEnumerable<ScheduleDto>> GetByScheduleUserId(int userId)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/ScheduleUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
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
    public async Task<IEnumerable<ScheduleDto>> GetByScheduleUser()
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();

            //if (token.Bearer is null)
            //{
            //    return [];
            //}

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Bearer);
            using var response = await httpClient.GetAsync($"{apiEndPoint}/Schedule");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<IEnumerable<ScheduleDto>>(_serializerOptions);
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
