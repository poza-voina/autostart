using Application.Strategies;
using AutoStart.Abstractions.Constants;
using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Services.Interfaces;
using AutoStart.StartPlugin.Strategies.Parameters;

namespace AutoStart.StartPlugin.Strategies;

public class ImportStrategy : StrategyWithoutInputBase<ImportStrategyParameters>
{
	private IConfigurationService _configurationService;
	private IFileManagerService _fileManagerService;

	public ImportStrategy(IConfigurationService configurationService, IFileManagerService fileManagerService)
	{
		Parameters = new ImportStrategyParameters();
		_configurationService = configurationService;
		_fileManagerService = fileManagerService;
	}

	public override void Run()
	{
		if (Parameters.PathToConfiguration is null)
		{
			throw new NotFoundException("Path to configuration-xml not found");
		}

		_configurationService.GetConfiguration(Parameters.PathToConfiguration);

		File.Copy(Parameters.PathToConfiguration,
			Path.Combine(_fileManagerService.GetPathToConfigurationDirectory(), FileNamesConstants.ConfigurationFileName));
	}
}
