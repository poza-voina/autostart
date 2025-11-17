using AutoStart.Abstractions.ArgumentStrategies.Interfaces;
using AutoStart.Abstractions.Exceptions;

namespace Application.Strategies;

public class StrategyFactory(IServiceProvider serviceProvider) : IStrategyFactory
{
	public IStrategy<IWithoutInputStrategy<TParams>, TParams> CreateWithoutData<TStrategy, TParams>()
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<IWithoutInputStrategy<TParams>, TParams>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;
	}

	public IStrategy<IWithInputStrategy<TParams, TInputData>, TParams> CreateWithData<TStrategy, TParams, TInputData>()
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<IWithInputStrategy<TParams, TInputData>, TParams>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;

	}
}
