using AutoStart.Abstractions.Plugin;
using CommandLine;

namespace AutoStart.EnvPlugin.ArgumentOptions;

public class EnvOptions : IRootArgument
{
	[Option("test")]
	public bool Test {  get; set; }
}
