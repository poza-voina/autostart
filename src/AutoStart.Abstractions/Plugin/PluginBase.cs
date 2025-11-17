using Application.ArgumentData;
using AutoStart.Abstractions.ArgumentData;
using AutoStart.Abstractions.Exceptions;
using CommandLine;
using System.Reflection;

namespace AutoStart.Abstractions.Plugin;

public abstract class PluginBase<TRootArgument> : IPlugin<TRootArgument> where TRootArgument : class, IRootArgument
{
	protected string[]? arguments;
	protected ParseArgumentsResult<TRootArgument>? parseResult;

	protected string[] Arguments
	{
		get => arguments ?? throw new NotFoundException("string arguments not found");
	}

	public TRootArgument GetRootArgument()
	{
		if (parseResult?.RootArgument is { } rootArgument)
		{
			return rootArgument;
		}

		throw new NotFoundException("root argument not found");
	}

	public IEnumerable<ArgSchema> RootArgumentSchema { get; } = typeof(TRootArgument)
			.GetProperties()
			.Select(p => new { Attr = p.GetCustomAttribute<OptionAttribute>(), IsBool = p.PropertyType.IsAssignableTo(typeof(bool)) })
			.Where(x => x != null)
			.Select(x => new ArgSchema { Name = $"--{x.Attr!.LongName}", IsBool = x.IsBool, HelpText = x.Attr.HelpText })
			.ToList();

	public TOptions GetRootArgumentOptions<TOptions>()
	{
		if (parseResult?.Kwargs is { } kwargs)
		{
			return ParseKwargs<TOptions>(kwargs);
		}

		throw new NotFoundException("root argument options not found");
	}

	public IPluginBase WithArguments(string[] args)
	{
		arguments = args;

		parseResult = ParseArguments(args);

		return this;
	}

	public abstract void Execute();

	protected ParseArgumentsResult<TRootArgument> ParseArguments(string[] args)
	{
		int? rootArgIndex = null;
		ArgSchema? argschema = null;

		var rootArgsf = RootArgumentSchema;

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

		TRootArgument? rootArg;
		if (argschema.IsBool)
		{
			rootArg = Parser.Default.ParseArguments<TRootArgument>([args[0]]).Value ?? throw new NotFoundException("root argument cant parse");
		}
		else
		{
			rootArg = Parser.Default.ParseArguments<TRootArgument>(args.Take(2)).Value ?? throw new NotFoundException("root argument cant parse");
		}

		return new ParseArgumentsResult<TRootArgument>
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