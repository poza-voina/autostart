using Application.Exceptions;
using Application.Strategies.Parameters;
using Application.XmlSchemas;
using System.Diagnostics.CodeAnalysis;

namespace Application.Strategies;

public abstract class StrategyBase<TParameters> : IStrategy<TParameters> where TParameters : IParameters
{
	protected TParameters? _parameters;
	protected bool isParametersInitialized = false;

	[MemberNotNull(nameof(_parameters))]
	protected void ValidateStrategy()
	{
		if (_parameters is null)
		{
			throw new NotFoundException("Параметры не найдены");
		}
	}

	public abstract void Run(Configuration configuration);

	public IStrategy<TParameters> WithParams(Action<TParameters> parameters)
	{
		NotFoundException.ThrowIfNull(_parameters);

		parameters(_parameters);
		isParametersInitialized = true;

		return this;
	}

	public virtual IStrategy<TParameters> WithParams(TParameters parameters)
	{
		_parameters = parameters;

		return this;
	}
}