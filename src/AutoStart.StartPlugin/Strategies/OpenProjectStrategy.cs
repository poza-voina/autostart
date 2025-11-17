using Application.Strategies;
using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Services.Interfaces;
using AutoStart.StartPlugin.Strategies.Parameters;
using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Strategies;

public class OpenProjectStrategy : StrategyWithInputBase<OpenProjectStrategyParameters, Configuration>
{
	private IStartApplicationService _startApplicationService;

	public OpenProjectStrategy(IStartApplicationService startApplicationService)
	{
		Parameters = new OpenProjectStrategyParameters
		{
			ProjectName = string.Empty
		};

		_startApplicationService = startApplicationService;
	}

	protected override void IternalRun(Configuration configuration)
	{
		if (configuration is null)
		{
			throw new NotFoundException("configuration not found");
		}

		var project = configuration.Projects.FirstOrDefault(x => x.Name == Parameters.ProjectName)
			?? throw new NotFoundException($"project with name = {Parameters.ProjectName} not found");

		var apps = configuration
			.Programs
			.Where(x => project.Start.Contains(x.Name));

		foreach (var item in apps)
		{
			_startApplicationService.StartApplication(item);
		}
	}
}