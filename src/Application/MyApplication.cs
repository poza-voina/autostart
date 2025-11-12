using Application.ArgumentData;
using Application.ArgumentOptions;
using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies;
using Application.Strategies.Parameters;
using Application.XmlSchemas;
using CommandLine;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Reflection;

namespace Application;

public class MyApplication(
	IConfigurationService configurationService,
	IStrategyFactory strategyFactory,
	IFileManagerService fileManager,
	ILogger<MyApplication> logger)
{
	private string? _fileName;

	public MyApplication WithConfiguration(string fileName)
	{
		_fileName = fileName;
		return this;
	}

	public Task Run(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Для работы приложения требуется ввести аргументы");
			return Task.CompletedTask;
		}

		var options = ParseArguments(args);

		var rootArgument = options.RootArgument;

		var exeDirectory = string.Empty;

		try
		{
			exeDirectory = Path.GetDirectoryName(AppContext.BaseDirectory);
			NotFoundException.ThrowIfNull(_fileName);
			NotFoundException.ThrowIfNull(exeDirectory);
		}
		catch
		{
			logger.LogCritical("not found configuration file");
			throw;
		}

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
				.CreateConfigurationStrategy<OpenProjectStrategy, OpenProjectStrategyParameters>()
				.WithParams(x => x.ProjectName = rootArgument.StartProject)
				.Run(GetConfiguration());
		}

		else if (rootArgument.StartApplication is { })
		{
			strategyFactory
				.CreateConfigurationStrategy<OpenApplicationStrategy, OpenApplicationStrategyParameters>()
				.WithParams(x => x.ProgramName = rootArgument.StartApplication)
				.Run(GetConfiguration());
		}

		else if (rootArgument.DisplayProjects)
		{
			strategyFactory
				.CreateConfigurationStrategy<DisplayProjectsStrategy, DisplayProjectsStrategyParameters>()
				.WithParams(ParseKwargs<DisplayProjectsOptions>(options.Kwargs).DisplayProjectsOptionsToParameters() ?? throw new NotFoundException("display projects params cant parse"))
				.Run(GetConfiguration());
		}

		else if (rootArgument.DisplayApplications)
		{
			strategyFactory
				.CreateConfigurationStrategy<DisplayApplicationStrategy, DisplayApplicationStrategyParameters>()
				.WithParams(ParseKwargs<DisplayApplicationsOptions>(options.Kwargs)?.DisplayApplicationOptionsToParameters() ?? throw new NotFoundException("display application params cant parse"))
				.Run(GetConfiguration());
		}

		return Task.CompletedTask;
	}

	private Configuration GetConfiguration()
	{
		NotFoundException.ThrowIfNull(_fileName);

		var path = Path.Combine(fileManager.GetPathToConfigurationDirectory(), _fileName);

		return configurationService.GetConfiguration(path);
	}

	private ParseArgumentsResult ParseArguments(string[] args)
	{
		int? rootArgIndex = null;
		ArgSchema? argschema = null;

		var rootArgsf = typeof(StartOptions)
			.GetProperties()
			.Select(p => new { Attr = p.GetCustomAttribute<OptionAttribute>(), IsBool = p.PropertyType.IsAssignableTo(typeof(bool)) })
			.Where(x => x != null)
			.Select(x => new ArgSchema { Name = $"--{x.Attr!.LongName}", IsBool = x.IsBool, HelpText = x.Attr.HelpText})
			.ToList();

		for (var i = 0; i < args.Length; i++)
		{
			var arg = args[i];
			var argf = rootArgsf.FirstOrDefault(x => x.Name == arg);
			if (argf is { })
			{
				rootArgIndex = i;
				argschema = argf;
			}
		}

		if (rootArgIndex is null || argschema is null)
		{
			throw new NotFoundException(
				$"root argument not found:\n" +
				string.Join("\n", rootArgsf.Select(x => $"{x.Name.PadRight(rootArgsf.Max(x => x.Name.Length))} {x.HelpText}"))
			);
		}

		StartOptions? rootArg = null;
		if (argschema.IsBool)
		{
			rootArg = Parser.Default.ParseArguments<StartOptions>([args[0]]).Value ?? throw new NotFoundException("root argument cant parse");
		}
		else
		{
			rootArg = Parser.Default.ParseArguments<StartOptions>(args.Take(2)).Value ?? throw new NotFoundException("root argument cant parse");
		}

		return new ParseArgumentsResult
		{
			RootArgument = rootArg,
			Kwargs = args.Skip(rootArgIndex.Value + 1).ToArray()
		};
	}

	private T ParseKwargs<T>(IEnumerable<string> kwargs)
	{
		return Parser.Default.ParseArguments<T>(kwargs).Value;
	}
}