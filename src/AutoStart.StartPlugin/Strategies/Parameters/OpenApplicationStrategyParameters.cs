using AutoStart.Abstractions.ArgumentStrategies.Interfaces;

namespace AutoStart.StartPlugin.Strategies.Parameters;

public class OpenApplicationStrategyParameters : IParameters
{
	public required string ProgramName { get; set; }
}
