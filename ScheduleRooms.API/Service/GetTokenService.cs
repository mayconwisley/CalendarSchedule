using Microsoft.IdentityModel.Tokens;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ScheduleRooms.API.Service;

public class GetTokenService : IGetTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;

    public GetTokenService(IUserService userService)
    {
        _configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();
        _userService = userService;

    }
    public async Task<TokenDto> Token(LoginDto login)
    {
        TokenDto tokenDto = new();
        if (login.Username is null || login.Password is null)
        {
            return new();
        }

        if (!await _userService.GetPassword(login))
        {
            return new();
        }

        string? strJWT = _configuration.GetSection("JWT")["Secret"];
        byte[] jwt = Encoding.UTF8.GetBytes(strJWT!);

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Acesso Api", "Acesso Api"),
                new Claim("Usuário", login.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwt),
                                                                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var strToken = tokenHandler.WriteToken(token);

        tokenDto.Bearer = strToken;

        return tokenDto;
    }
}
