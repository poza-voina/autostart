using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public interface IStrategy<TThis, TParams> where TThis : IStrategy<TThis, TParams>
{
	TThis WithParams(Action<TParams> parameters);
	TThis WithParams(TParams parameters);
}