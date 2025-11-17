using Application.Exceptions;

namespace Application.Strategies;

public abstract class StrategyWithoutInputBase<TParams> : IWithoutInputStrategy<TParams>
{
	private TParams? _parameters;

	protected TParams Parameters
	{
		get => _parameters ?? throw new NotFoundException("_parameters not found");
		set => _parameters = value;
	}

	public IWithoutInputStrategy<TParams> WithParams(Action<TParams> parameters)
	{
		NotFoundException.ThrowIfNull(_parameters);

		parameters.Invoke(_parameters);

		return this;
	}

	public IWithoutInputStrategy<TParams> WithParams(TParams parameters)
	{
		_parameters = parameters;

		return this;
	}

	public abstract void Run();
}