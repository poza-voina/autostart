using Application.Exceptions;
using Application.Strategies.Parameters;
using Application.XmlSchemas;
using System.ComponentModel;

namespace Application.Strategies;

public class StrategyFactory(IServiceProvider serviceProvider) : IStrategyFactory
{
	public IStrategy<TParameters, TInputData> Create<TStrategy, TParameters, TInputData>()
		where TStrategy : IStrategy<TParameters, TInputData>
		where TParameters : IParameters
		where TInputData : class, IData
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<TParameters, TInputData>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;
	}

	public IStrategy<TParameters, Configuration> CreateConfigurationStrategy<TStrategy, TParameters>()
		where TStrategy : IStrategy<TParameters, Configuration>
		where TParameters : IParameters
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<TParameters, Configuration>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;
	}

	public IStrategy<TParameters, StrategyWithoutData> CreateWithoutData<TStrategy, TParameters>()
		where TStrategy : IStrategy<TParameters, StrategyWithoutData>
		where TParameters : IParameters
	{
		var strategy = serviceProvider.GetService(typeof(TStrategy)) as IStrategy<TParameters, StrategyWithoutData>;

		NotFoundException.ThrowIfNull(strategy);

		return strategy;
	}
}
