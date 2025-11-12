using Application.Constants;
using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies.Parameters;

namespace Application.Strategies;

public class ImportStrategy : WithoutParamsStrategyBase<ImportStrategyParameters>
{
	private IConfigurationService _configurationService;
	private IFileManagerService _fileManagerService;

	public ImportStrategy(IConfigurationService configurationService, IFileManagerService fileManagerService)
	{
		Parameters = new ImportStrategyParameters();
		_configurationService = configurationService;
		_fileManagerService = fileManagerService;
	}

	public override void Run(StrategyWithoutData? configuration = null)
	{
		ValidateStrategy();

		if (Parameters.PathToConfiguration is null)
		{
			throw new NotFoundException("Path to configuration-xml not found");
		}

		_configurationService.GetConfiguration(Parameters.PathToConfiguration);

		File.Copy(Parameters.PathToConfiguration,
			Path.Combine(_fileManagerService.GetPathToConfigurationDirectory(), FileNamesConstants.ConfigurationFileName));
	}
}
