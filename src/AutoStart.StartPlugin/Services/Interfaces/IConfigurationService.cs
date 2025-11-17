using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Services.Interfaces;

public interface IConfigurationService
{
	Configuration GetConfiguration(string path);
}
