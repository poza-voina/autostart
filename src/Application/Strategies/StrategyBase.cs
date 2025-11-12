using Application.Exceptions;
using Application.Strategies.Parameters;
using System.Diagnostics.CodeAnalysis;

namespace Application.Strategies;

public abstract class StrategyBase<TParameters, TInputData> : IStrategy<TParameters, TInputData>
	where TParameters : IParameters where TInputData : class, IData
{
	private TParameters? _parameters;

	protected TParameters Parameters
	{
		get
		{
			if (_parameters is null)
			{
				throw new NotFoundException("parameters not found");
			}
			return _parameters;
		}

		set
		{
			_parameters = value;
		}
	}

	protected bool isParametersInitialized = false;

	[MemberNotNull(nameof(_parameters))]
	protected void ValidateParameters()
	{
		if (_parameters is null)
		{
			throw new NotFoundException("Параметры не найдены");
		}
	}

	public abstract void Run(TInputData? configuration = null);

	public IStrategy<TParameters, TInputData> WithParams(Action<TParameters> parameters)
	{
		NotFoundException.ThrowIfNull(_parameters);

		parameters(_parameters);
		isParametersInitialized = true;

		return this;
	}

	public virtual IStrategy<TParameters, TInputData> WithParams(TParameters parameters)
	{
		_parameters = parameters;

		return this;
	}
}