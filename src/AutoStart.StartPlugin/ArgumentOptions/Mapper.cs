using Application.ArgumentOptions;
using AutoStart.StartPlugin.Strategies.Parameters;

namespace AutoStart.StartPlugin.ArgumentOptions;

public static class Mapper
{
	public static DisplayProjectsStrategyParameters DisplayProjectsOptionsToParameters(this DisplayProjectsOptions src)
	{
		return new DisplayProjectsStrategyParameters
		{
			Search = src.Search,
			WithApplications = src.WithApplications
		};
	}

	public static DisplayApplicationStrategyParameters DisplayApplicationOptionsToParameters(this DisplayApplicationsOptions src)
	{
		return new DisplayApplicationStrategyParameters
		{
			Search = src.Search
		};
	}
}