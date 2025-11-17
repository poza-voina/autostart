using AutoStart.Abstractions.ArgumentStrategies.Interfaces;

namespace AutoStart.StartPlugin.Strategies.Parameters;

public class OpenProjectStrategyParameters : IParameters
{
	public required string ProjectName { get; set; }
}
