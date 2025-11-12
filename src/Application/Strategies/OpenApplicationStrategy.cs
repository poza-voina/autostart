using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies.Parameters;
using Application.XmlSchemas;

namespace Application.Strategies;

public class OpenApplicationStrategy : StrategyBase<OpenApplicationStrategyParameters>
{
	private IStartApplicationService _startApplicationService;

	public OpenApplicationStrategy(IStartApplicationService startApplicationService)
	{
		_parameters = new OpenApplicationStrategyParameters
		{
			ProgramName = string.Empty
		};

		_startApplicationService = startApplicationService;
	}

	public override void Run(Configuration configuration)
	{
		ValidateStrategy();

		var program = configuration.Programs.FirstOrDefault(x => x.Name == _parameters.ProgramName)
			?? throw new NotFoundException($"program with name = {_parameters.ProgramName} not found");

		_startApplicationService.StartApplication(program);
	}
}