using ScheduleRooms.API.Model;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.MappingDto.LoginDtos;

public static class LoginMappingDto
{
    public static LoginDto ConvertLoginApiToLoginDto(this LoginApi loginApi)
    {
        return new LoginDto
        {
            Username = loginApi.Username,
            Password = loginApi.Password
        };
    }
    public static LoginApi ConvertLoginDtoToLoginApi(this LoginDto loginDto)
    {
        return new LoginApi
        {
            Username = loginDto.Username,
            Password = loginDto.Password
        };
    }
}
