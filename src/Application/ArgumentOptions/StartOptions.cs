using CommandLine;

namespace Application.ArgumentOptions;

public class StartOptions
{
	[Option("start-project", HelpText = "Название проекта")]
	public string? StartProject { get; set; }

	[Option("sp", HelpText = "Название проекта")]
	public string? StartProjectAliase { get => StartProject; set => StartProject = value; }

	[Option("display-projects", HelpText = "Вывести проекты")]
	public bool DisplayProjects { get; set; }

	[Option("dp", HelpText = "Вывести проекты")]
	public bool DisplayProjectsAliase { get => DisplayProjects; set => DisplayProjects = value; }

	[Option("display-applications")]
	public bool DisplayApplications { get; set; }

	[Option("da")]
	public bool DisplayApplicationsAliase { get => DisplayApplications; set => DisplayApplications = value; }

	[Option("start-application")]
	public string? StartApplication { get; set; }

	[Option("sa")]
	public string? StartApplicationAliase { get => StartApplication; set => StartApplication = value; }
}
