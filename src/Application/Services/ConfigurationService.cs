using Application.Exceptions;
using Application.Services.Interfaces;
using Application.XmlSchemas;
using Microsoft.Extensions.Logging;
using System.Xml.Serialization;

namespace Application.Services;

public class ConfigurationService(ILogger<ConfigurationService> logger) : IConfigurationService
{
	public Configuration GetConfiguration(string path)
	{
		var serializer = new XmlSerializer(typeof(Configuration));

		if (!File.Exists(path))
		{
			logger.LogCritical("cant read config file with path = {}", path);
			throw new NotFoundException($"config file not found with path = {path}");
		}

		using var stream = File.OpenRead(path);

		var config = serializer.Deserialize(stream) as Configuration;
		if (config is null)
		{
			logger.LogCritical("cant parse config file with path = {}", path);
			throw new NotFoundException($"config file not found with path = {path}");
		}

		return config;
	}
}
