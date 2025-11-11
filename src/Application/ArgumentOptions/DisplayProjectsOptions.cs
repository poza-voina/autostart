using CommandLine;

namespace Application.ArgumentOptions;

public class DisplayProjectsOptions
{
	[Option("search")]
	public string? Search { get; set; }

	[Option("se")]
	public string? SearchAliase { get => Search; set => Search = value; }

	[Option("with-applications")]
	public bool WithApplications { get; set; } = false;

	[Option("wa")]
	public bool WithApplicationsAliase { get => WithApplications; set => WithApplications = value; }
}
