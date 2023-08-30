using AgendaSalas.Models.Dtos;
using AgendaSalas.Web.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AgendaSalas.Web.Service
{
    public class SalaService : ISalaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _serializerOptions;
        private const string? apiEndPoint = "api/Sala/Todas";
        private readonly SalaDto salaDto = new();
        private readonly SalaView salaView = new();

        public SalaService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public Task<SalaDto> Create(SalaDto salaDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SalaView> GetAll(int page = 1, int size = 25, string search = "")
        {
            try
            {
                using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
                using var response = await httpClient.GetAsync($"{apiEndPoint}?page={page}&size={size}&search={search}");


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

        public Task<SalaDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SalaDto> Update(SalaDto salaDto)
        {
            throw new NotImplementedException();
        }
    }
}
