using Application.ArgumentData;
using Application.ArgumentOptions;
using Application.Exceptions;
using Application.Services.Interfaces;
using Application.Strategies;
using Application.Strategies.Parameters;
using CommandLine;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Application;

public class MyApplication(
	IConfigurationService configurationService,
	IStrategyFactory strategyFactory,
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

		var path = Path.Combine(exeDirectory, _fileName);

		var config = configurationService.GetConfiguration(path);

		if (rootArgument.StartProject is { })
		{
			strategyFactory
				.Create<OpenProjectStrategy, OpenProjectStrategyParameters>()
				.WithParams(x => x.ProjectName = rootArgument.StartProject)
				.Run(config);
		}

		if (rootArgument.StartApplication is { })
		{
			strategyFactory
				.Create<OpenApplicationStrategy, OpenApplicationStrategyParameters>()
				.WithParams(x => x.ProgramName = rootArgument.StartApplication)
				.Run(config);
		}

		if (rootArgument.DisplayProjects)
		{
			strategyFactory
				.Create<DisplayProjectsStrategy, DisplayProjectsStrategyParameters>()
				.WithParams(ParseKwargs<DisplayProjectsOptions>(options.Kwargs).DisplayProjectsOptionsToParameters() ?? throw new NotFoundException("display projects params cant parse"))
				.Run(config);
		}

		if (rootArgument.DisplayApplications)
		{
			strategyFactory
				.Create<DisplayApplicationStrategy, DisplayApplicationStrategyParameters>()
				.WithParams(ParseKwargs<DisplayApplicationsOptions>(options.Kwargs)?.DisplayApplicationOptionsToParameters() ?? throw new NotFoundException("display application params cant parse"))
				.Run(config);
		}

		return Task.CompletedTask;
	}

	private ParseArgumentsResult ParseArguments(string[] args)
	{
		int? rootArgIndex = null;
		ArgSchema? argschema = null;

		var rootArgsf = typeof(StartOptions)
			.GetProperties()
			.Select(p => new { Attr = p.GetCustomAttribute<OptionAttribute>(), IsBool = p.PropertyType.IsAssignableTo(typeof(bool)) })
			.Where(x => x != null)
			.Select(x => new ArgSchema { Name = $"--{x.Attr!.LongName}", IsBool = x.IsBool })
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
			throw new NotFoundException("root argument not found");
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