using AutoStart.Abstractions.ArgumentStrategies.Interfaces;

namespace AutoStart.StartPlugin.Strategies.Parameters;

public class ImportStrategyParameters : IParameters
{
	public string? PathToConfiguration { get; set; }
}
