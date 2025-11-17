using AutoStart.Abstractions.Attributes;
using AutoStart.Abstractions.Plugin;
using AutoStart.EnvPlugin.ArgumentOptions;

[assembly: Plugin("EnvPlugin", "Плагин для работы с переменными окружения")]

namespace AutoStart.EnvPlugin;

public class EnvPlugin : PluginBase<EnvOptions>
{
	public override void Execute()
	{
		Console.WriteLine(GetRootArgument().Test);
	}
}
