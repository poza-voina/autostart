using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public interface IStrategyFactory
{
	IStrategy<IWithoutInputStrategy<TParams>, TParams> CreateWithoutData<TStrategy, TParams>();

	IStrategy<IWithInputStrategy<TParams, TInputData>, TParams> CreateWithData<TStrategy, TParams, TInputData>();
}