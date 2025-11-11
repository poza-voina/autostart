using Application;
using Application.Extensions;
using Application.Services;
using Application.Services.Interfaces;
using Application.Strategies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

internal class Program
{
	private static void Main(string[] args)
	{
		var host = Host.CreateDefaultBuilder(args)
		.ConfigureServices((context, services) =>
		{
			services.AddSingleton<MyApplication>();
			services.AddSingleton<IConfigurationService, ConfigurationService>();
			services.AddSingleton<IStrategyFactory, StrategyFactory>(x => new StrategyFactory(x.CreateScope().ServiceProvider));
			services.AddScoped<IStartApplicationService, StartApplicationService>();


			services.AddStrategyFactory(Assembly.GetExecutingAssembly());

			var a = services.Select(x => x.ServiceType).ToList();
		})
		.Build();

		var services = host.Services;

		host.Start();

		services.GetRequiredService<MyApplication>().WithConfiguration("programs.xml").Run(args);
	}
}