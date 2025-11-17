using AutoStart.Abstractions.ArgumentStrategies.Interfaces;
using AutoStart.Abstractions.Exceptions;

namespace Application.Strategies;

public abstract class StrategyWithInputBase<TParams, TInputData> : IWithInputStrategy<TParams, TInputData>
{

	private TParams? _parameters;
	private TInputData? _inputData;

	protected TParams Parameters
	{
		get => _parameters ?? throw new NotFoundException("_parameters not found");
		set => _parameters = value;
	}

	protected TInputData InputData
	{
		get => _inputData ?? throw new NotFoundException("_inputData not found");
		set => _inputData = value;
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

	public IWithInputStrategy<TParams, TInputData> WithInputData(TInputData inputData)
	{
		InputData = inputData;

		return this;
	}

	public void Run()
	{
		IternalRun(InputData);
	}

	protected abstract void IternalRun(TInputData inputData);
}
