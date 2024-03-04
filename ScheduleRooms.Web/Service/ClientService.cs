using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Models;
using ScheduleRooms.Web.Service.Interface;
using System.Net;
using System.Net.Http.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class ClientService : IClientService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/Client";

    public ClientService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<ClientDto> Create(ClientDto clientDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            StringContent stringContent = new(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if ((response.IsSuccessStatusCode))
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();

                    var client = await JsonSerializer.DeserializeAsync<ClientDto>(resApi, _serializerOptions);
                    if (client is not null)
                    {
                        return client;
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

    public async Task<ClientView> GetAll(int page = 1, int size = 10, string search = "")
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/All?page={page}&size={size}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                ClientView? clientView = await response.Content.ReadFromJsonAsync<ClientView>(_serializerOptions);
                return clientView ??= new();
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

    public async Task<ClientDto> GetById(int id)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var clientDto = await response.Content.ReadFromJsonAsync<ClientDto>(_serializerOptions);
                return clientDto;
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

    public async Task<ClientDto> Update(ClientDto clientDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

            StringContent stringContent = new StringContent(JsonSerializer.Serialize(clientDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{apiEndPoint}/{clientDto.Id}", stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var client = await JsonSerializer.DeserializeAsync<ClientDto>(resApi, _serializerOptions);
                    if (client is not null)
                    {
                        return client;
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
