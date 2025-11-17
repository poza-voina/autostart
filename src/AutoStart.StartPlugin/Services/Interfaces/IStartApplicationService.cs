using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Services.Interfaces;

public interface IStartApplicationService
{
	public void StartApplication(ProgramType program);
}