using Application;
using Application.Extensions;
using Application.PluginIntegration;
using Application.Strategies;
using AutoStart.Abstractions.ArgumentStrategies.Interfaces;
using AutoStart.StartPlugin.Constants;
using AutoStart.StartPlugin.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

internal class Program
{
	private static void Main(string[] args)
	{
		var path = Path.Combine(AppContext.BaseDirectory, "Plugins");
		var pluginAssemblies = PluginLoader.LoadPluginAssemblies(path) ?? throw new Exception("Плагины не загрузились");

		var builder = Host.CreateDefaultBuilder(args)
			.ConfigureServices((context, services) =>
			{
				services.AddSingleton<IPluginRunner, PluginRunner>();

				services.AddSingleton<MyApplication>();
				services.AddSingleton<IStrategyFactory, StrategyFactory>(x => new StrategyFactory(x.CreateScope().ServiceProvider));
				services.AddStrategyFactory(pluginAssemblies);
				foreach (var assembly in pluginAssemblies)
				{
					PluginLoader.LoadPluginServices(services, assembly);
				}
			});

		builder.ConfigureLogging(x => { x.ClearProviders(); });

		var host = builder.Build();

		var services = host.Services;

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(Path.Combine(services.GetRequiredService<IFileManagerService>().GetRootDirectory(), "autostart.log"))
			.CreateLogger();

		host.Start();


		args = ["--test"];

		try
		{
			Log.Information("Application starting...");
			services.GetRequiredService<MyApplication>().Run(args);
			Log.Information("Application finished successfully");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
			Log.Fatal(ex, "Application terminated unexpectedly");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}