using Application.Services.Interfaces;
using Application.XmlSchemas;
using System;
using System.Diagnostics;

namespace Application.Services;

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
