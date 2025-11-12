using Application.Constants;
using Application.Services.Interfaces;

namespace Application.Services;

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
