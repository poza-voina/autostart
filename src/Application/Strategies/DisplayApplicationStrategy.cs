using Application.Strategies.Parameters;
using Autostart;

namespace Application.Strategies;

public class DisplayApplicationStrategy : StrategyBase<DisplayApplicationStrategyParameters>
{
	public DisplayApplicationStrategy()
	{
		_parameters = new DisplayApplicationStrategyParameters();
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy();

		var applicationNames = configuration.Programs.Select(x => x.Name).ToList();

		foreach (var item in applicationNames)
		{
			Console.WriteLine(item);
		}
	}
}

