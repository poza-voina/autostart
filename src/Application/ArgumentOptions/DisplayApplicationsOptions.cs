using CommandLine;

namespace Application.ArgumentOptions;

public class DisplayApplicationsOptions
{
	[Option("search")]
	public string? Search { get; set; }

	[Option("se")]
	public string? SearchAliase { get => Search; set => Search = value; }
}
