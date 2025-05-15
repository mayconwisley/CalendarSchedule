using CalendarSchedule.Models.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CalendarSchedule.API.Helper;

public static class ExceptionHandler
{
	public static async Task<Result<T>> TryAsync<T>(Func<Task<Result<T>>> action)
	{
		try
		{
			return await action();
		}
		catch (DbUpdateException dbEx)
		{
			return Result.Failure<T>(Error.Internal($"Erro no banco de dados: {dbEx.InnerException?.Message ?? dbEx.Message}"));
		}
		catch (ValidationException valEx)
		{
			return Result.Failure<T>(Error.Validation(valEx.Message));
		}
		catch (Exception ex)
		{
			return Result.Failure<T>(Error.Internal($"Erro inesperado: {ex.Message}"));
		}
	}
}
