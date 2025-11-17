namespace Application.ArgumentData;

public class ParseArgumentsResult<TRootArgument>
{
	public required TRootArgument? RootArgument { get; set; }
	public required IEnumerable<string> Kwargs { get; set; }
}