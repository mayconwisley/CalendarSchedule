using ScheduleRooms.Models.Dtos;
using ScheduleRooms.Web.Service.Interface;
using System.Text;
using System.Text.Json;

namespace ScheduleRooms.Web.Service;

public class LoginService : ILoginService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _serializerOptions;
    private const string? apiEndPoint = "api/Login";

    public LoginService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<string> Token(LoginDto loginDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            StringContent stringContent = new(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync(apiEndPoint, stringContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    using Stream resApi = await response.Content.ReadAsStreamAsync();
                    var token =  JsonSerializer.Deserialize<string>(resApi);

                    if (token is not null)
                    {
                        return token;
                    }
                }
                else
                {
                    response.EnsureSuccessStatusCode();
                }
            }
            return string.Empty;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
