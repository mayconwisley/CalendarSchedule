using System.Diagnostics.CodeAnalysis;

namespace CalendarSchedule.Models.Abstractions;

public class Result
{
	public bool IsSuccess { get; }
	public bool IsFailure => !IsSuccess;
	public Error Error { get; }

	public static Result Success() => new(true, Error.None(string.Empty));
	public static Result Failure(Error error) => new(false, error);

	public static Result<T> Success<T>(T value) => new(value, true, Error.None(string.Empty));
	public static Result<T> Failure<T>(Error error) => new(default, false, error);

	public static Result<T> Create<T>(T? value)
	{
		if (value is null)
			return Failure<T>(Error.NullValue(string.Empty));

		return Success(value);
	}

	protected Result(bool isSuccess, Error error)
	{
		if (isSuccess && error != Error.None(""))
			throw new InvalidOperationException("O resultado bem-sucedido não pode ter erro.");

		if (!isSuccess && error == Error.None(""))
			throw new InvalidOperationException("O resultado com falha deve ter um erro.");

		IsSuccess = isSuccess;
		Error = error;
	}
}

public class Result<T> : Result
{
	private readonly T? _value;

	protected internal Result(T? value, bool isSuccess, Error error) : base(isSuccess, error) => _value = value;

	[NotNull]
	public T Value => _value! ?? throw new InvalidOperationException("O resultado não tem valor");

	public static implicit operator Result<T>(T? value) => Create(value);
}