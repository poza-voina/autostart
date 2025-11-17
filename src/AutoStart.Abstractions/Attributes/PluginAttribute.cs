namespace AutoStart.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Assembly)]
public class PluginAttribute : Attribute
{
	public string Name { get; }
	public string? Description { get; }

	public PluginAttribute(string name, string? description)
	{
		Name = name;
		Description = description;
	}
}
