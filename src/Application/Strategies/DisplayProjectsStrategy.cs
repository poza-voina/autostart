using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public class DisplayProjectsStrategy : ConfigurationStrategyBase<DisplayProjectsStrategyParameters>
{
	public DisplayProjectsStrategy()
	{
		Parameters = new DisplayProjectsStrategyParameters();
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy(configuration);

		var projects = configuration.Projects.ToList();

		if (Parameters.WithApplications)
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

		else if (!Parameters.WithApplications)
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

