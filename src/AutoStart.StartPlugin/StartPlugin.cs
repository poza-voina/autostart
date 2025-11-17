using Application.ArgumentData;
using Application.ArgumentOptions;
using AutoStart.Abstractions.ArgumentData;
using AutoStart.Abstractions.ArgumentStrategies.Interfaces;
using AutoStart.Abstractions.Attributes;
using AutoStart.Abstractions.Exceptions;
using AutoStart.Abstractions.Plugin;
using AutoStart.StartPlugin.ArgumentOptions;
using AutoStart.StartPlugin.Services.Interfaces;
using AutoStart.StartPlugin.Strategies;
using AutoStart.StartPlugin.Strategies.Parameters;
using AutoStart.StartPlugin.XmlSchemas;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Reflection;

[assembly: Plugin("Autostart", "Плагин для запуска приложений")]

namespace AutoStart.StartPlugin;

public class StartPlugin(
	IStrategyFactory strategyFactory,
	IFileManagerService fileManager,
	IConfigurationService configurationService) : PluginBase<StartOptions>
{
	private string _fileName = "programs.xml";

	public override void Execute()
	{ 
		var rootArgument = GetRootArgument();

		if (rootArgument.ImportPath is { })
		{
			strategyFactory
				.CreateWithoutData<ImportStrategy, ImportStrategyParameters>()
				.WithParams(x => x.PathToConfiguration = rootArgument.ImportPath)
				.Run();
		}

		if (rootArgument.StartProject is { })
		{
			strategyFactory
				.CreateWithData<OpenProjectStrategy, OpenProjectStrategyParameters, Configuration>()
				.WithParams(x => x.ProjectName = rootArgument.StartProject)
				.WithInputData(GetConfiguration())
				.Run();
		}

		else if (rootArgument.StartApplication is { })
		{
			strategyFactory
				.CreateWithData<OpenApplicationStrategy, OpenApplicationStrategyParameters, Configuration>()
				.WithParams(x => x.ProgramName = rootArgument.StartApplication)
				.WithInputData(GetConfiguration())
				.Run();
		}

		else if (rootArgument.DisplayProjects)
		{
			strategyFactory
				.CreateWithData<DisplayProjectsStrategy, DisplayProjectsStrategyParameters, Configuration>()
				.WithParams(GetRootArgumentOptions<DisplayProjectsOptions>().DisplayProjectsOptionsToParameters())
				.WithInputData(GetConfiguration())
				.Run();
		}

		else if (rootArgument.DisplayApplications)
		{
			strategyFactory
				.CreateWithData<DisplayApplicationStrategy, DisplayApplicationStrategyParameters, Configuration>()
				.WithParams(GetRootArgumentOptions<DisplayApplicationsOptions>().DisplayApplicationOptionsToParameters())
				.WithInputData(GetConfiguration())
				.Run();
		}
	}

	private Configuration GetConfiguration()
	{
		NotFoundException.ThrowIfNull(_fileName);

		var path = Path.Combine(fileManager.GetPathToConfigurationDirectory(), _fileName);

		return configurationService.GetConfiguration(path);
	}
}