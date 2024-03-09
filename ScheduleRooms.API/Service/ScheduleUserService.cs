using ScheduleRooms.API.MappingDto.ScheduleUserDtos;
using ScheduleRooms.API.Repository.Interface;
using ScheduleRooms.API.Service.Interface;
using ScheduleRooms.Models.Dtos;

namespace ScheduleRooms.API.Service
{
    public class ScheduleUserService(IScheduleUserRepository scheduleUserRepository) : IScheduleUserService
    {
        private readonly IScheduleUserRepository _scheduleUserRepository = scheduleUserRepository;

        public async Task Create(ScheduleUserDto schedulesUserDto)
        {
            await _scheduleUserRepository.Create(schedulesUserDto.ConvertDtoToSchedule());
        }

        public async Task Delete(int id)
        {
            var scheduleEntity = await GetById(id);
            if (scheduleEntity is not null)
            {
                await _scheduleUserRepository.Delete(scheduleEntity.Id);
            }
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetAll(int page, int size, string search)
        {
            var scheduleEntity = await _scheduleUserRepository.GetAll(page, size, search);
            return scheduleEntity.ConvertSchedulesToDto();
        }

        public async Task<ScheduleUserDto> GetById(int id)
        {
            var scheduleEntity = await _scheduleUserRepository.GetById(id);
            return scheduleEntity.ConvertScheduleToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetBySchedule()
        {
            var schedyleEntity = await _scheduleUserRepository.GetBySchedule();
            return schedyleEntity.ConvertSchedulesToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleActive()
        {
            var schedyleEntity = await _scheduleUserRepository.GetByScheduleActive();
            return schedyleEntity.ConvertSchedulesToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleActiveClientId(int clientId, DateTime dateSalected)
        {
            var scheduleEntity = await _scheduleUserRepository.GetByScheduleActiveClientId(clientId, dateSalected);
            return scheduleEntity.ConvertSchedulesToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleActiveClientIdUserId(int clientId, int userId, DateTime dateSalected)
        {
            var scheduleEntity = await _scheduleUserRepository.GetByScheduleActiveClientIdUserId(clientId, userId, dateSalected);
            return scheduleEntity.ConvertSchedulesToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleDateUserId(int userId, DateTime dateSalected)
        {
            var scheduleEntity = await _scheduleUserRepository.GetByScheduleDateUserId(userId, dateSalected);
            return scheduleEntity.ConvertSchedulesToDto();
        }

        public async Task<IEnumerable<ScheduleUserDto>> GetByScheduleUserId(int userId)
        {
            var scheduleEntity = await _scheduleUserRepository.GetByScheduleUserId(userId);
            return scheduleEntity.ConvertSchedulesToDto();
        }

        public async Task<int> TotalSchedules(string search)
        {
            var totalScheduleUser = await _scheduleUserRepository.TotalSchedules(search);
            return totalScheduleUser;
        }

        public async Task Update(ScheduleUserDto schedulesUserDto)
        {
            await _scheduleUserRepository.Update(schedulesUserDto.ConvertDtoToSchedule());
        }
    }
}
