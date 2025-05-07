using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;
using CalendarSchedule.Web.Service.Interface;


namespace CalendarSchedule.Web.Service;

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

    public async Task<Result<TokenDto>> Token(LoginDto loginDto)
    {
        try
        {
            using var httpClient = _httpClientFactory.CreateClient("ConexaoApi");
            StringContent stringContent = new(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(apiEndPoint, stringContent);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadFromJsonAsync<TokenDto>(_serializerOptions);

                if (token is null)
                    return Result.Failure<TokenDto>(Error.NotFound("Token não gerado"));

                return Result.Success(token);
            }
            else
            {
                var error = await response.Content.ReadFromJsonAsync<Error>(_serializerOptions);
                if (error is null)
                    return Result.Failure<TokenDto>(Error.NotFound("Token não gerado"));
                return Result.Failure<TokenDto>(error);
            }
        }
        catch (Exception ex)
        {
            return Result.Failure<TokenDto>(Error.Internal($"Erro interno: {ex.Message}"));
        }
    }
}
