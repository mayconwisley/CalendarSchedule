using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Models;
using CalendarSchedule.Web.Service.Interface;

namespace CalendarSchedule.Web.Service;

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

    public async Task<Result<ScheduleDto>> Create(ScheduleCreateDto scheduleCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ScheduleDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleCreateDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var schedule = await JsonSerializer.DeserializeAsync<ScheduleDto>(resApi, _serializerOptions);
                if (schedule is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Success(schedule);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Failure<ScheduleDto>(error);
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
                    return Result.Failure<bool>(Error.NotFound("Schedule Nulo"));
                return Result.Failure<bool>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<PagedResultView<ScheduleDto>>> GetAll(int page, int size, string search)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<PagedResultView<ScheduleDto>>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleView = await response.Content.ReadFromJsonAsync<PagedResultView<ScheduleDto>>(_serializerOptions);
                if (scheduleView is null)
                    return Result.Failure<PagedResultView<ScheduleDto>>(Error.NotFound("Schedule Nulo"));
                return Result.Success(scheduleView);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<PagedResultView<ScheduleDto>>(Error.NotFound("Schedule Nulo"));
                return Result.Failure<PagedResultView<ScheduleDto>>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Result<ScheduleDto>> GetById(int id)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ScheduleDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var scheduleDto = await response.Content.ReadFromJsonAsync<ScheduleDto>(_serializerOptions);
                if (scheduleDto is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Success(scheduleDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Failure<ScheduleDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<Result<PagedResultView<ScheduleDto>>> GetBySchedule()
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResultView<ScheduleDto>>> GetByScheduleActive()
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResultView<ScheduleDto>>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
    {
        throw new NotImplementedException();
    }

    public Task<Result<int>> TotalSchedules(string search)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ScheduleDto>> Update(ScheduleCreateDto scheduleCreateDto)
    {
        try
        {
            var token = await _tokenStorageService.GetToken();
            if (token.IsFailure)
                return Result.Failure<ScheduleDto>(token.Error);

            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value.Bearer);

            StringContent stringContent = new(JsonSerializer.Serialize(scheduleCreateDto), Encoding.UTF8, "application/json");
            using var response = await httpClient.PutAsync($"{apiEndPoint}/{scheduleCreateDto.Id}", stringContent);
            if (response.IsSuccessStatusCode)
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var scheduleDto = await JsonSerializer.DeserializeAsync<ScheduleDto>(resApi, _serializerOptions);
                if (scheduleDto is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Success(scheduleDto);
            }
            else
            {
                using Stream resApi = await response.Content.ReadAsStreamAsync();
                var error = await JsonSerializer.DeserializeAsync<Error>(resApi, _serializerOptions);
                if (error is null)
                    return Result.Failure<ScheduleDto>(Error.NotFound("Schedule Nulo"));
                return Result.Failure<ScheduleDto>(error);
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
