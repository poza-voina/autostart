using Application.Exceptions;
using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public class DisplayApplicationStrategy : StrategyWithInputBase<DisplayApplicationStrategyParameters, Configuration>
{
	public DisplayApplicationStrategy()
	{
		Parameters = new DisplayApplicationStrategyParameters();
	}

	public override void Run(Configuration configuration)
	{
		if (configuration is null)
		{
			throw new NotFoundException("configuration not found");
		}

		var applicationNames = configuration.Programs.Select(x => x.Name).ToList();

		foreach (var item in applicationNames)
		{
			Console.WriteLine(item);
		}
	}
}

