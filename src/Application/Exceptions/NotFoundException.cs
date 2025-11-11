using System.Diagnostics.CodeAnalysis;

namespace Application.Exceptions;

public class NotFoundException : Exception
{
	public NotFoundException() { }
	public NotFoundException(string message) : base(message) { }
	public NotFoundException(string message, Exception innerException) { }

	public static void ThrowIfNull([NotNull] object? data)
	{
		if (data is null)
		{
			throw new NotImplementedException($"Не найдено");
		}
	}
}
