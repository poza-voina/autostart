using Application.Constants;
using CommandLine;

namespace Application.ArgumentOptions;

public class DisplayApplicationsOptions
{
	[Option("search", HelpText = DisplayApplicationOptionsConstants.SearchApplicationHelpText)]
	public string? Search { get; set; }

	[Option("se", HelpText = DisplayApplicationOptionsConstants.SearchApplicationHelpText)]
	public string? SearchAliase { get => Search; set => Search = value; }
}
