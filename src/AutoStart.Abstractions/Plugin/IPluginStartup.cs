using Microsoft.Extensions.DependencyInjection;

namespace AutoStart.Abstractions.Plugin;

public interface IPluginStartup
{
	void ConfigureServices(IServiceCollection services);
}