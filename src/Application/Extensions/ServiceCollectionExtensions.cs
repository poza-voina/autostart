using Application.Strategies;
using AutoStart.Abstractions.ArgumentStrategies.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
	public static void AddStrategyFactory(this IServiceCollection services, IEnumerable<Assembly> assemblies)
	{
		foreach ( var item in assemblies)
		{
			ProduceAssembly(services, item);
		}
	}

	private static void ProduceAssembly(IServiceCollection services, Assembly assembly)
	{
		var types = assembly.GetTypes();

		var parametersTypes = types
		.Where(
			x => x.GetInterfaces()
			.Any(x => x.IsAssignableTo(typeof(IParameters)))).ToList();

		var inputDataTypes = types
		.Where(
			x => x.GetInterfaces()
			.Any(x => x.IsAssignableTo(typeof(IData)))).ToList();


		var configurationStrategies = parametersTypes
			.SelectMany(
				parameter => inputDataTypes,
				(parameter, inputData) => typeof(StrategyWithInputBase<,>)
				.MakeGenericType(parameter, inputData))
			.ToList();

		var strategiesWithoutData = parametersTypes
			.Select(x => typeof(StrategyWithoutInputBase<>).MakeGenericType(x));

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
