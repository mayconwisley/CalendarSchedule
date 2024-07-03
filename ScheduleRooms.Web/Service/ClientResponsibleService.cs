using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ClientResponsibleService : IClientResponsibleService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorageService _tokenStorageService;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/ClientResponsible";

    public ClientResponsibleService(IHttpClientFactory httpClientFactory, ITokenStorageService tokenStorageService)
    {
        _httpClientFactory = httpClientFactory;
        _tokenStorageService = tokenStorageService;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ClientResponsibleDto> Create(ClientResponsibleCreateDto clientResponsibleCreateDto)
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

            StringContent stringContent = new(JsonSerializer.Serialize(clientResponsibleCreateDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if ((response.IsSuccessStatusCode))
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();

                    var clientResponsible = await JsonSerializer.DeserializeAsync<ClientResponsibleDto>(resApi, _serializerOptions);
                    if (clientResponsible is not null)
                    {
                        return clientResponsible;
                    }
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                    return new();
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

    public async Task<ClientResponsibleView> GetAll(int page = 1, int size = 10, string search = "")
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
                ClientResponsibleView? clientResponsibleView = await response.Content.ReadFromJsonAsync<ClientResponsibleView>(_serializerOptions);
                return clientResponsibleView ??= new();
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

    public async Task<ClientResponsibleDto> GetById(int id)
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
                var clientResponsibleDto = await response.Content.ReadFromJsonAsync<ClientResponsibleDto>(_serializerOptions);
                return clientResponsibleDto ??= new();
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

    public async Task<ClientResponsibleDto> Update(ClientResponsibleCreateDto clientResponsibleCreateDto)
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

            StringContent stringContent = new StringContent(JsonSerializer.Serialize(clientResponsibleCreateDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{clientResponsibleCreateDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var clientResponsible = await JsonSerializer.DeserializeAsync<ClientResponsibleDto>(resApi, _serializerOptions);
                    if (clientResponsible is not null)
                    {
                        return clientResponsible;
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
