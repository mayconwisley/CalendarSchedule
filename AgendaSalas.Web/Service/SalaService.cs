using AgendaSalas.Models.Dtos;
using AgendaSalas.Web.Models;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace AgendaSalas.Web.Service
{
    public class SalaService : ISalaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private const string? apiEndPoint = "api/Sala";
        private readonly SalaDto salaDto = new();
        private readonly SalaView salaView = new();

        public SalaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<SalaDto> Create(SalaDto salaDto)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

                StringContent stringContent = new(JsonSerializer.Serialize(salaDto), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using Stream resApi = await response.Content.ReadAsStreamAsync();
                        var sala = await JsonSerializer.DeserializeAsync<SalaDto>(resApi, _serializerOptions);
                        if (sala is not null)
                        {
                            return sala;
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
                using (var response = await httpClient.DeleteAsync($"{apiEndPoint}/{id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                }
                return new();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<SalaView> GetAll(int page = 1, int size = 25, string search = "")
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}/Todas?page={page}&size={size}&search={search}");


                if (response.IsSuccessStatusCode)
                {
                    var salaView = await response.Content.ReadFromJsonAsync<SalaView>(_serializerOptions);
                    return salaView ?? new();
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

        public async Task<SalaDto> GetById(int id)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var salaDto = await response.Content.ReadFromJsonAsync<SalaDto>(_serializerOptions);
                    return salaDto ?? new();
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

        public Task<SalaDto> Update(SalaDto salaDto)
        {
            throw new NotImplementedException();
        }
    }
}
