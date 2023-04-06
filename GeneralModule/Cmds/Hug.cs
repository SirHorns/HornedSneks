namespace GeneralModule;

internal class Hug: Command
{
    public string[] AcceptUrls { get; set; }
    public string[] RejectUrls { get; set; }
    
    public Hug(string[] acceptUrls, string[] rejectUrls) 
        : base("", Array.Empty<string>(), Array.Empty<string>())
    {
        AcceptUrls = acceptUrls;
        RejectUrls = rejectUrls;
    }

    public string GetImageAcceptUrl()
    {
        return AcceptUrls[new Random().Next(0, AcceptUrls.Length-1)];
    }
    
    public string GetImageRejectUrl()
    {
        return RejectUrls[new Random().Next(0, RejectUrls.Length-1)];
    }
}