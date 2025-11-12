using Application.Strategies.Parameters;

namespace Application.Strategies;

public abstract class WithoutParamsStrategyBase<TParameters> : StrategyBase<TParameters, StrategyWithoutData>
	where TParameters : IParameters
{
	public abstract override void Run(StrategyWithoutData? configuration = null);

	protected void ValidateStrategy()
	{
		ValidateParameters();
	}

}
