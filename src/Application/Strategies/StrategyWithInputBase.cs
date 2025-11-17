using Application.Exceptions;

namespace Application.Strategies;

public abstract class StrategyWithInputBase<TParams, TInputData> : IWithInputStrategy<TParams, TInputData>
{

	private TParams? _parameters;

	protected TParams Parameters
	{
		get => _parameters ?? throw new NotFoundException("_parameters not found");
		set => _parameters = value;
	}

	public IWithInputStrategy<TParams, TInputData> WithParams(Action<TParams> parameters)
	{
		NotFoundException.ThrowIfNull(_parameters);

		parameters.Invoke(_parameters);

		return this;
	}

	public IWithInputStrategy<TParams, TInputData> WithParams(TParams parameters)
	{
		_parameters = parameters;
		
		return this;
	}

	public abstract void Run(TInputData inputData);
}
