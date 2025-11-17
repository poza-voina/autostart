using AutoStart.Abstractions.Exceptions;
using AutoStart.StartPlugin.Services.Interfaces;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using AutoStart.StartPlugin.XmlSchemas;

namespace AutoStart.StartPlugin.Services;

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
