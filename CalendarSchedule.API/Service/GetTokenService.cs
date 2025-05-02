using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.Models.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CalendarSchedule.API.Service;

public class GetTokenService(IUserService _userService) : IGetTokenService
{
    private readonly IConfiguration _configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();

    public async Task<Result<TokenDto>> Token(LoginDto login)
    {
        if (login.Username is null || login.Password is null)
            return Result.Failure<TokenDto>(Error.BadRequest("Usuário e/ou senha inválidos"));

        var isUsername = await _userService.IsUsername(login.Username);
        if (isUsername.IsFailure)
            return Result.Failure<TokenDto>(isUsername.Error);

        var isPassword = await _userService.IsPassword(login);
        if (isPassword.IsFailure)
            return Result.Failure<TokenDto>(isPassword.Error);

        string? strJWT = _configuration.GetSection("JWT")["Secret"];
        byte[] jwt = Encoding.UTF8.GetBytes(strJWT!);

        var tokenConfig = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim("Acesso Api", "Acesso Api"),
                new Claim("Usuário", login.Username)
            ]),
            Expires = DateTime.UtcNow.AddHours(4),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(jwt),
                                                                SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenConfig);
        var strToken = tokenHandler.WriteToken(token);

        TokenDto tokenDto = new()
        {
            Bearer = strToken
        };

        return Result.Success(tokenDto);
    }
}
