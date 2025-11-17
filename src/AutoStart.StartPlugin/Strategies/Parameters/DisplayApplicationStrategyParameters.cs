using AutoStart.Abstractions.ArgumentStrategies.Interfaces;

namespace AutoStart.StartPlugin.Strategies.Parameters;

public class DisplayApplicationStrategyParameters : IParameters
{
	public string? Search { get; set; }
}