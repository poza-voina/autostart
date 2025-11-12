using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public class DisplayApplicationStrategy : ConfigurationStrategyBase<DisplayApplicationStrategyParameters>
{
	public DisplayApplicationStrategy()
	{
		Parameters = new DisplayApplicationStrategyParameters();
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy(configuration);

		var applicationNames = configuration.Programs.Select(x => x.Name).ToList();

		foreach (var item in applicationNames)
		{
			Console.WriteLine(item);
		}
	}
}

