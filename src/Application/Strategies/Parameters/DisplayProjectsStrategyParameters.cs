namespace Application.Strategies.Parameters;

public class DisplayProjectsStrategyParameters : IParameters
{
	public string? Search { get; set; }
	public bool WithApplications { get; set; } = true;
}
