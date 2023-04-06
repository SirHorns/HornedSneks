namespace GeneralModule;

internal class Rape: Command
{
    public string[] Overpower { get; set; }
    public string[] ButGay { get; set; }
    
    public Rape(string[] overpower, string[] butgay) 
        : base("", Array.Empty<string>(), Array.Empty<string>())
    {
        Overpower = overpower;
        ButGay = butgay;
    }
    
    public string GetImageOverpowerUrl()
    {
        return Overpower[new Random().Next(0, Overpower.Length-1)];
    }
    
    public string GetImageGayUrl()
    {
        return ButGay[new Random().Next(0, ButGay.Length-1)];
    }
}