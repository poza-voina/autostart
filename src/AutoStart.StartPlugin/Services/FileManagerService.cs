using AutoStart.StartPlugin.Services.Interfaces;

namespace AutoStart.StartPlugin.Services;

public class FileManagerService : IFileManagerService
{
	public string GetRootDirectory()
	{
		return AppContext.BaseDirectory;
	}

	public string GetPathToConfigurationDirectory()
	{
		return GetRootDirectory();
	}
}
