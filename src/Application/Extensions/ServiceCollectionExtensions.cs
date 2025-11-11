using Application.Strategies;
using Application.Strategies.Parameters;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddStrategyFactory(this IServiceCollection services, Assembly assembly)
	{
		var types = assembly.GetTypes();

		var strategyBaseTypes = types
		.Where(
			x => x.GetInterfaces()
			.Any(x => x.IsAssignableTo(typeof(IParameters))))
		.Select(x => typeof(StrategyBase<>).MakeGenericType(x))
		.ToArray();

		var strategyTypes = types.Where(x => strategyBaseTypes.Any(y => x.IsAssignableTo(y)));


		foreach (var item in strategyTypes)
		{
			services.AddScoped(item);
		}
	}
}
