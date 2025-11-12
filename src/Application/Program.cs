using Application;
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
				services.AddSingleton<MyApplication>();
				services.AddSingleton<IConfigurationService, ConfigurationService>();
				services.AddSingleton<IStrategyFactory, StrategyFactory>(x => new StrategyFactory(x.CreateScope().ServiceProvider));
				services.AddScoped<IStartApplicationService, StartApplicationService>();

				services.AddStrategyFactory(Assembly.GetExecutingAssembly());

				var a = services.Select(x => x.ServiceType).ToList();
			});

		builder.ConfigureLogging(x => { x.ClearProviders(); });

		Log.Logger = new LoggerConfiguration()
			.WriteTo.File(Path.Combine(AppContext.BaseDirectory, "autostart.log"))
			.CreateLogger();

		var host = builder.Build();

		var services = host.Services;

		host.Start();

		try
		{
			Log.Information("Application starting...");
			services.GetRequiredService<MyApplication>().WithConfiguration("programs.xml").Run(args);
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