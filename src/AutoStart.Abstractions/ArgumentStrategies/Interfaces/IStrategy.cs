namespace AutoStart.Abstractions.ArgumentStrategies.Interfaces;

public interface IStrategy<TThis, TParams> where TThis : IStrategy<TThis, TParams>
{
	TThis WithParams(Action<TParams> parameters);
	TThis WithParams(TParams parameters);
}