using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public interface IStrategyFactory
{
	IStrategy<TParameters, TInputData> Create<TStrategy, TParameters, TInputData>()
		where TStrategy : IStrategy<TParameters, TInputData>
		where TParameters : IParameters
		where TInputData : class, IData;

	IStrategy<TParameters, Configuration> CreateConfigurationStrategy<TStrategy, TParameters>()
		where TStrategy : IStrategy<TParameters, Configuration>
		where TParameters : IParameters;

	IStrategy<TParameters, StrategyWithoutData> CreateWithoutData<TStrategy, TParameters>()
		where TStrategy : IStrategy<TParameters, StrategyWithoutData>
		where TParameters : IParameters;
}