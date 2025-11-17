namespace AutoStart.Abstractions.ArgumentData;

public class ArgSchema
{
	public required string Name { get; set; }
	public bool IsBool { get; set; }
	public string? HelpText { get; set; }
}
