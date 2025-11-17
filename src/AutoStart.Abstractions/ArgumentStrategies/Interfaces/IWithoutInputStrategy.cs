using AutoStart.Abstractions.ArgumentStrategies.Interfaces;

namespace Application.Strategies;

public interface IWithoutInputStrategy<TParams> : IStrategy<IWithoutInputStrategy<TParams>, TParams>
{
	void Run();
}
