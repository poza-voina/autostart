using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public class OpenProjectStrategy : ConfigurationStrategyBase<OpenProjectStrategyParameters>
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

	public override void Run(Configuration configuration)
	{
		ValidateStrategy(configuration);

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