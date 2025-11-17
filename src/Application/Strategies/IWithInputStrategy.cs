namespace Application.Strategies;

public interface IWithInputStrategy<TParams, TInputData> : IStrategy<IWithInputStrategy<TParams, TInputData>, TParams>
{
	void Run(TInputData inputData);
}