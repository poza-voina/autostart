using Application.Strategies;
using Application.Strategies.Parameters;
using Application.XmlSchemas;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddStrategyFactory(this IServiceCollection services, Assembly assembly)
	{
		var types = assembly.GetTypes();

		var parametersTypes = types
		.Where(
			x => x.GetInterfaces()
			.Any(x => x.IsAssignableTo(typeof(IParameters)))).ToList();

		var configurationStrategies = parametersTypes
			.Select(x => typeof(StrategyBase<,>)
			.MakeGenericType(x, typeof(Configuration)));

		var strategiesWithoutData = parametersTypes
			.Select(x => typeof(StrategyBase<,>).MakeGenericType(x, typeof(StrategyWithoutData)));

		var strategyTypes = types
			.Where(
				x => configurationStrategies.Any(y => x.IsAssignableTo(y)) ||
				strategiesWithoutData.Any(y => x.IsAssignableTo(y)));

		foreach (var item in strategyTypes)
		{
			services.AddScoped(item);
		}
	}
}
