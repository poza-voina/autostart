using Application.Strategies;
using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Services.Interfaces;
using AutoStart.StartPlugin.Strategies.Parameters;
using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Strategies;

public class OpenApplicationStrategy : StrategyWithInputBase<OpenApplicationStrategyParameters, Configuration>
{
	private IStartApplicationService _startApplicationService;

	public OpenApplicationStrategy(IStartApplicationService startApplicationService)
	{
		Parameters = new OpenApplicationStrategyParameters
		{
			ProgramName = string.Empty
		};

		_startApplicationService = startApplicationService;
	}

	protected override void IternalRun(Configuration configuration)
	{
		if (configuration is null)
		{
			throw new NotFoundException("configuration not found");
		}

		var program = configuration.Programs.FirstOrDefault(x => x.Name == Parameters.ProgramName)
			?? throw new NotFoundException($"program with name = {Parameters.ProgramName} not found");

		_startApplicationService.StartApplication(program);
	}
}
