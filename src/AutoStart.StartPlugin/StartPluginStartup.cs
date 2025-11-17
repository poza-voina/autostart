using AutoStart.Abstractions.Plugin;
using AutoStart.StartPlugin.Services;
using AutoStart.StartPlugin.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AutoStart.StartPlugin;

public class StartPluginStartup : IPluginStartup
{
	public void ConfigureServices(IServiceCollection services)
	{
		services.AddScoped<IConfigurationService, ConfigurationService>();
		services.AddScoped<IStartApplicationService, StartApplicationService>();
		services.AddScoped<IFileManagerService, FileManagerService>();
	}
}
