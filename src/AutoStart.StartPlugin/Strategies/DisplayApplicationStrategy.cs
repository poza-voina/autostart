using Application.Strategies;
using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Strategies.Parameters;
using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Strategies;

public class DisplayApplicationStrategy : StrategyWithInputBase<DisplayApplicationStrategyParameters, Configuration>
{
	public DisplayApplicationStrategy()
	{
		Parameters = new DisplayApplicationStrategyParameters();
	}

	protected override void IternalRun(Configuration configuration)
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

