using Application.Strategies;
using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Strategies.Parameters;
using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Strategies;

public class DisplayProjectsStrategy : StrategyWithInputBase<DisplayProjectsStrategyParameters, Configuration>
{
	public DisplayProjectsStrategy()
	{
		Parameters = new DisplayProjectsStrategyParameters();
	}

	protected override void IternalRun(Configuration configuration)
	{
		if (configuration is null)
		{
			throw new NotFoundException("configuration not found");
		}

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

