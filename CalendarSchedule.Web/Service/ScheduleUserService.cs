using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

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

    public async Task<Result<ScheduleUserDto>> Create(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ScheduleUserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserCreateDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var scheduleUser = await JsonSerializer.DeserializeAsync<ScheduleUserDto>(resApi, _serializerOptions);
                if (scheduleUser is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUser);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<ScheduleUserDto>(error);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<Result<bool>> Delete(int scheduleId, int userId)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<bool>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.DeleteAsync($"{apiEndPoint}/{scheduleId}/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return Result.Success(true);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<bool>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<bool>(error);
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
    public async Task<Result<PagedResultView<ScheduleUserDto>>> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();
            //if (token.IsFailure)
            //    return Result.Failure<PagedResultView<ScheduleUserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserView = await response.Content.ReadFromJsonAsync<PagedResultView<ScheduleUserDto>>(_serializerOptions);
                if (scheduleUserView is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUserView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<PagedResultView<ScheduleUserDto>>(error);

            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<ScheduleUserDto>> GetById(int scheduleId, int userId)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();
            //if (token.IsFailure)
            //    return Result.Failure<ScheduleUserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/{scheduleId}/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<ScheduleUserDto>(_serializerOptions);
                if (scheduleUserDto is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUserDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<ScheduleUserDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleUserDateStart(DateOnly dateSelected)
    {
        try
        {
            //var token = await _tokenStorageService.GetToken();
            //if (token.IsFailure)
            //    return Result.Failure<PagedResultView<ScheduleUserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/DateStart/{dateSelected:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<PagedResultView<ScheduleUserDto>>(_serializerOptions);
                if (scheduleUserDto is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUserDto);

            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<PagedResultView<ScheduleUserDto>>(error);
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<Result<ScheduleUserDto>> Update(ScheduleUserCreateDto scheduleUserCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ScheduleUserDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleUserCreateDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleUserCreateDto.ScheduleId}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var scheduleUser = await JsonSerializer.DeserializeAsync<ScheduleUserDto>(resApi, _serializerOptions);
                if (scheduleUser is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUser);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleUserDto>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<ScheduleUserDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleId(int scheduleId)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<ScheduleUserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/{scheduleId}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<PagedResultView<ScheduleUserDto>>(_serializerOptions);
                if (scheduleUserDto is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUserDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<PagedResultView<ScheduleUserDto>>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<PagedResultView<ScheduleUserDto>>> GetByScheduleUserDatePeriod(DateOnly dateStart, DateOnly dateEnd)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<ScheduleUserDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/Period/{dateStart:yyyy-MM-dd}/{dateEnd:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleUserDto = await response.Content.ReadFromJsonAsync<PagedResultView<ScheduleUserDto>>(_serializerOptions);
                if (scheduleUserDto is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Success(scheduleUserDto);

            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ScheduleUserDto>>(Error.NotFound("Schedule User Nulo"));
                return Result.Failure<PagedResultView<ScheduleUserDto>>(error);
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
}
