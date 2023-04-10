using Newtonsoft.Json;

namespace RetardFilter;

public class FilterConfig
{
    public int Delay { get; set; }
    public int Interval { get; set; }
    public string[] Filter { get; set; }

    public FilterConfig()
    {
        Delay = 30 * 60;
        Interval = 15 * 60000;
        Filter = new[]{""};
        
        Console.WriteLine("==Filter Config Loaded==\n" +
                          $"Delay: {Delay/60}m\n" +
                          $"Interval: {Interval/60000}m\n" +
                          $"Filter: {Filter}\n" +
                          "========================");
    }

    public FilterConfig(int delay, int interval, string[] filter)
    {
        Delay = delay * 60;
        Interval = interval * 60000;
        Filter = filter;
        
        Console.WriteLine("==Filter Config Loaded==\n" +
                          $"Delay: {Delay/60}m\n" +
                          $"Interval: {Interval/60000}m\n" +
                          $"Filter: {Filter}\n" +
                          "========================");
    }

    public void WriteFile()
    {
        File.WriteAllText(Path.GetFullPath("data/Medusae/RetardFilter/FilterConfig.json"), JsonConvert.SerializeObject(this, Formatting.Indented));
    }
}