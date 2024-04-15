namespace ScheduleRooms.API.Service.Interface;

public interface IGetTokenService
{
    public Task<string> Token(string username, string password);
}
