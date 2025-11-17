using AutoStart.Abstractions.Attributes;
using AutoStart.Abstractions.Exceptions;
using AutoStart.Abstractions.Plugin;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.PluginIntegration;

public static class PluginLoader
{
	public static IEnumerable<Assembly>? LoadPluginAssemblies(string path)
	{
		var result = new List<Assembly>();

		if (!Directory.Exists(path))
		{
			Console.WriteLine("directory with plugins not found");
			return null;
		}

		var dllFiles = Directory.GetFiles(path, "*.dll", SearchOption.TopDirectoryOnly);

		foreach (var dll in dllFiles)
		{
			try
			{
				var assembly = Assembly.LoadFrom(dll);

				if (assembly.GetCustomAttribute<PluginAttribute>() is { })
				{
					result.Add(assembly);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"failed to load {dll}: {ex.Message}");
			}
		}

		return result;
	}

	public static void LoadPluginServices(IServiceCollection services, Assembly assembly)
	{
		var startupType = assembly
			.GetTypes()
			.FirstOrDefault(t =>
				typeof(IPluginStartup).IsAssignableFrom(t) &&
				!t.IsAbstract &&
				!t.IsInterface);

		NotFoundException.ThrowIfNull(startupType);

		var startup = (IPluginStartup)Activator.CreateInstance(startupType)!;
		startup.ConfigureServices(services);

		var pluginType = assembly.GetTypes()
				.FirstOrDefault(
					t => !t.IsAbstract && !t.IsInterface &&
						t.GetInterfaces().Any(i => i.IsGenericType &&
						i.GetGenericTypeDefinition() == typeof(IPlugin<>)));

		NotFoundException.ThrowIfNull(pluginType);

		var pluginInterface = pluginType.GetInterfaces()
			.First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPlugin<>));

		services.AddTransient(pluginInterface, pluginType);
		services.AddTransient(typeof(IPluginBase), pluginType);
	}
}