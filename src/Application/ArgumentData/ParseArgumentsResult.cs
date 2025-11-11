using Application.ArgumentOptions;

namespace Application.ArgumentData;

public class ParseArgumentsResult
{
	public required StartOptions RootArgument { get; set; }
	public required IEnumerable<string> Kwargs { get; set; }
}