using AutoStart.StartPlugin.Services.Interfaces;
using AutoStart.StartPlugin.XmlSchemas;
using System.Diagnostics;

namespace AutoStart.StartPlugin.Services;

public class StartApplicationService : IStartApplicationService
{
	public void StartApplication(ProgramType program)
	{
		var uri = program.UriType switch
		{
			UriTypeEnum.Steam => $"steam://rungameid/{program.Uri}",
			_ => program.Uri
		};

		var info = new ProcessStartInfo
		{
			FileName = uri,
			UseShellExecute = true
		};

		Process.Start(info);
	}
}
