using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

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

	public override void Run(Configuration configuration)
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
