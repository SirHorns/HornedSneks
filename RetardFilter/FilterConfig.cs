using Newtonsoft.Json;

namespace RetardFilter;

public class FilterConfig
{
    public double Delay { get; set; }
    public double Interval { get; set; }
    public string[] Filter { get; set; }

    public FilterConfig()
    {
        Delay = 1800;
        Interval = 15000;
        Filter = new[]{""};
    }

    public FilterConfig(double delay, double interval, string[] filter)
    {
        Delay = delay;
        var intv =interval * 1000;
        Interval = intv;
        Filter = filter;
    }

    public void WriteFile()
    {
        File.WriteAllText(Path.GetFullPath("data/Medusae/RetardFilter/FilterConfig.json"), JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}