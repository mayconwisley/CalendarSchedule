using AgendaSalas.Models.Dtos;
using AgendaSalas.Web.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Drawing;

namespace AgendaSalas.Web.Service
{
    public class AgendaService : IAgendaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private const string? apiEndPoint = "api/Agenda";

        public AgendaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<AgendaDto> Create(AgendaDto agendaDto)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

                StringContent stringContent = new(JsonSerializer.Serialize(agendaDto), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using Stream resApi = await response.Content.ReadAsStreamAsync();
                        var agenda = await JsonSerializer.DeserializeAsync<AgendaDto>(resApi, _serializerOptions);
                        if (agenda is not null)
                        {
                            return agenda;
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

        public async Task<AgendaView> GetAll(int page = 1, int size = 25, string search = "")
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}/Todas?page={page}&size={size}&search={search}");


                if (response.IsSuccessStatusCode)
                {
                    var agendaView = await response.Content.ReadFromJsonAsync<AgendaView>(_serializerOptions);
                    return agendaView ?? new();
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

        public async Task<AgendaDto> GetById(int id)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var agendaDto = await response.Content.ReadFromJsonAsync<AgendaDto>(_serializerOptions);
                    return agendaDto ?? new();
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

        public async Task<AgendaDto> Update(AgendaDto agendaDto)
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");

                StringContent stringContent = new(JsonSerializer.Serialize(agendaDto), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{apiEndPoint}/{agendaDto.Id}", stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using Stream resApi = await response.Content.ReadAsStreamAsync();
                        var agenda = await JsonSerializer.DeserializeAsync<AgendaDto>(resApi, _serializerOptions);
                        if (agenda is not null)
                        {
                            return agenda;
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

        public async Task<IEnumerable<AgendaDto>> GetByAgendaActive()
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}/AgendaAtiva");


                if (response.IsSuccessStatusCode)
                {
                    var agendaDto = await response.Content.ReadFromJsonAsync<IEnumerable<AgendaDto>>(_serializerOptions);
                    return agendaDto ??= new List<AgendaDto>();
                }
                else
                {
                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return new List<AgendaDto>();
                    }
                    response.EnsureSuccessStatusCode();
                }
                return new List<AgendaDto>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
