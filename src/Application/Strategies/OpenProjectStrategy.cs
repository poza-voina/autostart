using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies.Parameters;
using Autostart;

namespace Application.Strategies;

public class OpenProjectStrategy : StrategyBase<OpenProjectStrategyParameters>
{
	private IStartApplicationService _startApplicationService;

	public OpenProjectStrategy(IStartApplicationService startApplicationService)
	{
		_parameters = new OpenProjectStrategyParameters
		{
			ProjectName = string.Empty
		};

		_startApplicationService = startApplicationService;
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy();

		var project = configuration.Projects.FirstOrDefault(x => x.Name == _parameters.ProjectName)
			?? throw new NotFoundException($"project with name = {_parameters.ProjectName} not found");

		var apps = configuration
			.Programs
			.Where(x => project.Start.Contains(x.Name));

		foreach (var item in apps)
		{
			_startApplicationService.StartApplication(item);
		}
	}
}