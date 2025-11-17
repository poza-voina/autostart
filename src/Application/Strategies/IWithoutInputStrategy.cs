namespace Application.Strategies;

public interface IWithoutInputStrategy<TParams> : IStrategy<IWithoutInputStrategy<TParams>, TParams>
{
	void Run();
}
