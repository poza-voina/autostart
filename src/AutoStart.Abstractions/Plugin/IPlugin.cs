using AutoStart.Abstractions.ArgumentData;

namespace AutoStart.Abstractions.Plugin;

public interface IPluginBase
{
	public IEnumerable<ArgSchema> RootArgumentSchema { get; }
	IPluginBase WithArguments(string[] args);
	void Execute();
}

public interface IPlugin<TRootArgument> : IPluginBase where TRootArgument : class, IRootArgument
{
	TRootArgument GetRootArgument();
	TOptions GetRootArgumentOptions<TOptions>();
}