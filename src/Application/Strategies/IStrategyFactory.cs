using Application.Strategies.Parameters;

namespace Application.Strategies;

public interface IStrategyFactory
{
	IStrategy<TParameters> Create<TStrategy, TParameters>() where TStrategy : IStrategy<TParameters> where TParameters : IParameters;
}