using Application.Constants;
using CommandLine;

namespace Application.ArgumentOptions;

public class StartOptions
{
	[Option("start-project", HelpText = StartOptionsConstants.StartProjectHelpText)]
	public string? StartProject { get; set; }

	[Option("sp", HelpText = StartOptionsConstants.StartProjectHelpText)]
	public string? StartProjectAliase { get => StartProject; set => StartProject = value; }

	[Option("display-projects", HelpText = StartOptionsConstants.DisplayProjectHelpText)]
	public bool DisplayProjects { get; set; }

	[Option("dp", HelpText = StartOptionsConstants.DisplayProjectHelpText)]
	public bool DisplayProjectsAliase { get => DisplayProjects; set => DisplayProjects = value; }

	[Option("display-applications", HelpText = StartOptionsConstants.DisplayApplicationHelpText)]
	public bool DisplayApplications { get; set; }

	[Option("da", HelpText = StartOptionsConstants.DisplayApplicationHelpText)]
	public bool DisplayApplicationsAliase { get => DisplayApplications; set => DisplayApplications = value; }

	[Option("start-application", HelpText = StartOptionsConstants.StartApplicationHelpText)]
	public string? StartApplication { get; set; }

	[Option("sa", HelpText = StartOptionsConstants.StartApplicationHelpText)]
	public string? StartApplicationAliase { get => StartApplication; set => StartApplication = value; }

	[Option("import", HelpText = StartOptionsConstants.ImportHelpText)]
	public string? ImportPath { get; set; }

	[Option("--im", HelpText = StartOptionsConstants.ImportHelpText)]
	public string? ImportPathAliase { get => ImportPath; set => ImportPath = value; }
}
