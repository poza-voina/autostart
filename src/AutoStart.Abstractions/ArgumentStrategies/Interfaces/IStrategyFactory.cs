using Application.Strategies;

namespace AutoStart.Abstractions.ArgumentStrategies.Interfaces;

public interface IStrategyFactory
{
	IStrategy<IWithoutInputStrategy<TParams>, TParams> CreateWithoutData<TStrategy, TParams>();

	IStrategy<IWithInputStrategy<TParams, TInputData>, TParams> CreateWithData<TStrategy, TParams, TInputData>();
}