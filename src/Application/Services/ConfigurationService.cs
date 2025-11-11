using Application.Exceptions;
using Application.Services.Interfaces;
using Autostart;
using System.Xml.Serialization;

namespace Application.Services;

public class ConfigurationService : IConfigurationService
{
	public Configuration GetConfiguration(string path)
	{
		var serializer = new XmlSerializer(typeof(Configuration));

		using var stream = File.OpenRead(path);
		var config = serializer.Deserialize(stream) as Configuration;

		NotFoundException.ThrowIfNull(config);

		return config;
	}
}
