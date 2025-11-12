using Application;
using Application.Constants;
using Application.Extensions;
using Application.Services;
using Application.Services.Interfaces;
using Application.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = Host.CreateDefaultBuilder(args)
			.ConfigureServices((context, services) =>
			{
				services.AddSingleton<IFileManagerService, FileManagerService>();
				services.AddSingleton<MyApplication>();
				services.AddSingleton<IConfigurationService, ConfigurationService>();
				services.AddSingleton<IStrategyFactory, StrategyFactory>(x => new StrategyFactory(x.CreateScope().ServiceProvider));
				services.AddScoped<IStartApplicationService, StartApplicationService>();

				services.AddStrategyFactory(Assembly.GetExecutingAssembly());

				var a = services.Select(x => x.ServiceType).ToList();
			});

		builder.ConfigureLogging(x => { x.ClearProviders(); });

		var host = builder.Build();

		var services = host.Services;

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(Path.Combine(services.GetRequiredService<IFileManagerService>().GetRootDirectory(), FileNamesConstants.LogFileName))
			.CreateLogger();

		host.Start();

		try
		{
			Log.Information("Application starting...");
			services.GetRequiredService<MyApplication>().WithConfiguration(FileNamesConstants.ConfigurationFileName).Run(args);
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