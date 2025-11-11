using Application.Strategies.Parameters;
using Autostart;

namespace Application.Strategies;

public class DisplayProjectsStrategy : StrategyBase<DisplayProjectsStrategyParameters>
{
	public DisplayProjectsStrategy()
	{
		_parameters = new DisplayProjectsStrategyParameters();
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy();

		var projects = configuration.Projects.ToList();

		if (_parameters.WithApplications)
		{
			foreach (var item in projects)
			{
				Console.WriteLine($"=============");
				Console.WriteLine($"{item.Name}");
				Console.WriteLine($"-------------");

				foreach(var applicationName in item.Start)
				{
					Console.WriteLine(applicationName);
				}

				Console.WriteLine($"=============");
			}
		}

		else if (!_parameters.WithApplications)
		{
			foreach (var item in projects)
			{
				Console.WriteLine($"-------------");
				Console.WriteLine($"{item.Name}");
				Console.WriteLine($"-------------");
			}
		}
	}
}

