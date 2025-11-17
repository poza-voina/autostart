using Application.PluginIntegration;
using Microsoft.Extensions.Logging;
using System;

namespace Application;

public class MyApplication(ILogger<MyApplication> logger, IPluginRunner pluginRunner)
{
	public Task Run(string[] args)
	{
		if (args.Length == 0)
		{
			Console.WriteLine("Для работы приложения требуется ввести аргументы");
			return Task.CompletedTask;
		}

		pluginRunner.RunPlugin(args);

		return Task.CompletedTask;
	}
}