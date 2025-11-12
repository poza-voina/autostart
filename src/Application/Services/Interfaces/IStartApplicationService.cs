using Application.XmlSchemas;

namespace Application.Services.Interfaces;

public interface IStartApplicationService
{
	public void StartApplication(ProgramType program);
}