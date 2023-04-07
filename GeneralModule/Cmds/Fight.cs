namespace GeneralModule;

internal class Fight: Command
{
    public Fight(string[] success, string[] failed) : base("", Array.Empty<string>(), Array.Empty<string>())
    {
    }
}