namespace GeneralModule;

internal class GoodNight: Command
{
    public GoodNight(string[] videoUrls) 
        : base("", Array.Empty<string>(), videoUrls)
    {
    }
}