using Application.Exceptions;
using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public abstract class ConfigurationStrategyBase<TParameters> : StrategyBase<TParameters, Configuration> where TParameters : IParameters
{
	protected void ValidateStrategy(Configuration configuration)
	{
		ValidateParameters();

		if (configuration is null)
		{
			throw new NotFoundException("configuration not found");
		}
	}

	public abstract override void Run(Configuration configuration);
}