using Application.Exceptions;
using Application.Strategies.Parameters;
using System.ComponentModel;

namespace Application.Strategies;

public class StrategyFactory(IServiceProvider serviceProvider) : IStrategyFactory
{
	public IStrategy<TParameters> Create<TStrategy, TParameters>() where TStrategy : IStrategy<TParameters> where TParameters : IParameters
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<TParameters>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;
	}
}
