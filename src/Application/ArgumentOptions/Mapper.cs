using Application.Strategies.Parameters;

namespace Application.ArgumentOptions;

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