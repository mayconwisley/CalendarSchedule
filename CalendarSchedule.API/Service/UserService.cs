using CalendarSchedule.API.Helper;
using CalendarSchedule.API.MappingDto.LoginDtos;
using CalendarSchedule.API.MappingDto.UserDtos;
using CalendarSchedule.API.Repository.Interface;
using CalendarSchedule.API.Service.Interface;
using CalendarSchedule.API.Utility.Interface;
using CalendarSchedule.Models.Abstractions;
using CalendarSchedule.Models.Dtos;

namespace CalendarSchedule.API.Service;

public class UserService(IUserRepository _userRepository, IEncryptionUtility _encryptionUtility, IDecryptionUtility _decryptionUtility) : IUserService
{
	public async Task<Result<UserDto>> Create(UserCreateDto userCreateDto)
	{
		var exists = await ExistsByNameAsync(userCreateDto.Username!);
		if (exists.IsSuccess)
			return Result.Failure<UserDto>(Error.Conflict("Usuário já existe"));

		UserCreateDto userDto = new()
		{
			Name = userCreateDto.Name,
			Description = userCreateDto.Description,
			Username = userCreateDto.Username,
			Password = _encryptionUtility.Dado(userCreateDto.Password),
			Manager = userCreateDto.Manager,
			Active = userCreateDto.Active
		};

		return await ExceptionHandler.TryAsync(async () =>
		{
			var user = await _userRepository.Create(userDto.ConvertDtoCreateToUsere());
			if (user == null)
				return Result.Failure<UserDto>(Error.Internal("Falha ao criar usuário"));

			var dto = user.ConvertUserToDto();
			return Result.Success(dto);
		});
	}

	public async Task<Result<UserDto>> Delete(int id)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var deletedUserDto = await _userRepository.Delete(id);
			if (deletedUserDto is null)
				return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado para ser excluído"));

			var dto = deletedUserDto.ConvertUserToDto();
			return Result.Success(dto);
		});
	}

	public async Task<Result<bool>> ExistsByNameAsync(string username)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var exists = await _userRepository.ExistsByNameAsync(username);
			if (exists)
				return Result.Success(true);

			return Result.Failure<bool>(Error.NotFound("Usuário não encontrado"));
		});
	}

	public async Task<Result<PagedResult<UserDto>>> GetAll(int page, int size, string search)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var userEntity = await _userRepository.GetAll(page, size, search);
			if (userEntity is null)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			var totalUser = await _userRepository.TotalUser(search);
			if (totalUser <= 0)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			decimal totalData = totalUser;
			decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
			if (size == 1)
				totalPage = totalData;

			var dto = userEntity.ConvertUsersToDto();
			var userDto = new PagedResult<UserDto>(dto, totalData, page, totalPage, size);

			return Result.Success(userDto);
		});
	}

	public async Task<Result<UserDto>> GetById(int id)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var userDto = await _userRepository.GetById(id);
			if (userDto is null)
				return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

			UserDto user = new()
			{
				Id = userDto.Id,
				Name = userDto.Name,
				Description = userDto.Description,
				Username = userDto.Username,
				Password = _decryptionUtility.Dado(userDto.Password),
				Manager = userDto.Manager,
				Active = userDto.Active
			};

			return Result.Success(user);
		});
	}

	public async Task<Result<PagedResult<UserDto>>> GetManagerAll(int page, int size, string search)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var userEntity = await _userRepository.GetManagerAll(page, size, search);
			if (userEntity is null)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			var totalUser = await _userRepository.TotalUser(search);
			if (totalUser <= 0)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			decimal totalData = totalUser;
			decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
			if (size == 1)
				totalPage = totalData;

			var dto = userEntity.ConvertUsersToDto();
			var userDto = new PagedResult<UserDto>(dto, totalData, page, totalPage, size);

			return Result.Success(userDto);
		});
	}

	public async Task<Result<PagedResult<UserDto>>> GetManagerAllByUserCurrent(int page, int size, string search, string username)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var users = await _userRepository.GetManagerAllByUserCurrent(page, size, search, username);
			if (users is null)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			var totalUsers = await _userRepository.TotalUser(search);
			if (totalUsers <= 0)
				return Result.Failure<PagedResult<UserDto>>(Error.NotFound("Usuários não encontrado"));
			decimal totalData = totalUsers;
			decimal totalPage = (totalData / size) <= 0 ? 1 : Math.Ceiling(totalData / size);
			if (size == 1)
				totalPage = totalData;

			var dto = users.ConvertUsersToDto();
			var userDto = new PagedResult<UserDto>(dto, totalData, page, totalPage, size);

			return Result.Success(userDto);
		});
	}

	public async Task<Result<UserDto>> GetManagerUsername(string username)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var userEntity = await _userRepository.GetManagerUsername(username);
			if (userEntity is null)
				return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

			var dto = userEntity.ConvertUserToDto();
			return Result.Success(dto);

		});
	}

	public async Task<Result<bool>> IsPassword(LoginDto login)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var password = await _userRepository.GetPassword(login.ConvertLoginDtoToLoginApi());
			if (string.IsNullOrEmpty(password))
				return Result.Failure<bool>(Error.Validation("Senha inválida"));

			password = _decryptionUtility.Dado(password);

			if (password == login.Password)
				return Result.Success(true);

			return Result.Failure<bool>(Error.Validation("Erro ao validar senha"));
		});
	}

	public async Task<Result<bool>> IsUsername(string username)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var isUsername = await _userRepository.IsUsername(username);
			if (isUsername)
				return Result.Success(true);

			return Result.Failure<bool>(Error.NotFound("Usuário não encontrado"));
		});
	}

	public async Task<Result<int>> TotalUsers(string search)
	{
		return await ExceptionHandler.TryAsync(async () =>
		{
			var totalUser = await _userRepository.TotalUser(search);
			if (totalUser <= 0)
				return Result.Failure<int>(Error.NotFound("Nenhum usuário encontrado"));

			return Result.Success(totalUser);
		});
	}

	public async Task<Result<UserDto>> Update(UserUpdateDto userUpdateDto)
	{

		UserUpdateDto user = new()
		{
			Id = userUpdateDto.Id,
			Name = userUpdateDto.Name,
			Description = userUpdateDto.Description,
			Username = userUpdateDto.Username,
			Manager = userUpdateDto.Manager,
			Active = userUpdateDto.Active
		};
		if (!string.IsNullOrEmpty(userUpdateDto.Password))
			user.Password = _encryptionUtility.Dado(userUpdateDto.Password);

		return await ExceptionHandler.TryAsync(async () =>
		{
			var result = await _userRepository.Update(user.ConvertDtoUpdateToUsere());
			if (result is null)
				return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

			var dto = result.ConvertUserToDto();
			return Result.Success(dto);
		});
	}

	public async Task<Result<UserDto>> UpdatePatch(UserUpdateDto userUpdateDto)
	{
		var exists = await ExistsByNameAsync(userUpdateDto.Name!);
		if (exists.IsSuccess)
			return Result.Failure<UserDto>(Error.Validation("Usuário já existe"));

		UserUpdateDto user = new()
		{
			Id = userUpdateDto.Id,
			Name = userUpdateDto.Name,
			Description = userUpdateDto.Description,
			Username = userUpdateDto.Username,
			Password = _encryptionUtility.Dado(userUpdateDto.Password),
			Manager = userUpdateDto.Manager,
			Active = userUpdateDto.Active
		};

		return await ExceptionHandler.TryAsync(async () =>
		{
			var result = await _userRepository.UpdatePatch(user.ConvertDtoUpdateToUsere());
			if (result is null)
				return Result.Failure<UserDto>(Error.NotFound("Nenhum usuário encontrado"));

			var dto = result.ConvertUserToDto();
			return Result.Success(dto);
		});
	}
}
