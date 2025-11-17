using AutoStart.Abstractions.Exceptions;
using AutoStart.Abstractions.Plugin;
using Microsoft.Extensions.DependencyInjection;

namespace Application.PluginIntegration;

public class PluginRunner(IServiceProvider serviceProvider) : IPluginRunner
{
	public void RunPlugin(string[] args)
	{
		var arg = args.First();

		var plugin = serviceProvider
			.GetServices<IPluginBase>()
			.Select(x => new { Plugin = x, Schema = x.RootArgumentSchema.Select(x => x.Name) })
			.FirstOrDefault(x => x.Schema.Contains(arg)) ?? throw new NotFoundException("plugin not found");

		plugin.Plugin.WithArguments(args).Execute();
	}
}