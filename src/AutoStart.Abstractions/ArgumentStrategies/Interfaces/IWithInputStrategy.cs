namespace AutoStart.Abstractions.ArgumentStrategies.Interfaces;

public interface IWithInputStrategy<TParams, TInputData> : IStrategy<IWithInputStrategy<TParams, TInputData>, TParams>
{
	IWithInputStrategy<TParams, TInputData> WithInputData(TInputData inputData);
	void Run();
}