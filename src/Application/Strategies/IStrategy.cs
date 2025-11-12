using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public interface IStrategy<TParameters> where TParameters : IParameters
{
	IStrategy<TParameters> WithParams(Action<TParameters> parameters);
	IStrategy<TParameters> WithParams(TParameters parameters);
	void Run(Configuration configuration);
}