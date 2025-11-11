using Autostart;

namespace Application.Services.Interfaces;

public interface IConfigurationService
{
	Configuration GetConfiguration(string path);
}
