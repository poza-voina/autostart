using Application.Constants;
using CommandLine;

namespace Application.ArgumentOptions;

public class DisplayProjectsOptions
{
	[Option("search", HelpText = DisplayProjectOptionsConstants.SearchProjectHelpText)]
	public string? Search { get; set; }

	[Option("se", HelpText = DisplayProjectOptionsConstants.SearchProjectHelpText)]
	public string? SearchAliase { get => Search; set => Search = value; }

	[Option("with-applications", HelpText = DisplayProjectOptionsConstants.WithApplicationsHelpText)]
	public bool WithApplications { get; set; } = false;

	[Option("wa", HelpText = DisplayProjectOptionsConstants.WithApplicationsHelpText)]
	public bool WithApplicationsAliase { get => WithApplications; set => WithApplications = value; }
}
