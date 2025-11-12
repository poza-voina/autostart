using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public interface IStrategy<TParameters, TInputData> where TParameters : IParameters where TInputData : class, IData
{
	IStrategy<TParameters, TInputData> WithParams(Action<TParameters> parameters);
	IStrategy<TParameters, TInputData> WithParams(TParameters parameters);
	void Run(TInputData? configuration = null);
}