using System.Threading.Channels;
using Discord;
using Nadeko.Snake;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetardFilter;


/// <summary>
/// 
/// </summary>
public sealed class RetardFilter : Snek
{
    public override string Prefix { get; } = ".rtf";

    private FilterConfig _filterConfig;

    private List<FilterMessage> NoNoMessages;
    private string ConfigFile { get; set; } = "data/Medusae/RetardFilter/FilterConfig.json";
    private string FilterFile  { get; set; }= "data/Medusae/RetardFilter/Filter.json";
    private string CacheFile { get; set; } = "data/Medusae/RetardFilter/FilterCache.json";
    
    private string[] Filter { get; set; }

    public override ValueTask InitializeAsync()
    {
        HouseKeeping();
        return base.InitializeAsync();
    }

    public override ValueTask<bool> ExecOnMessageAsync(IGuild guild, IUserMessage msg)
    {
        
        FilterMsg(guild, msg);
        return base.ExecOnMessageAsync(guild, msg);
    }

    private void FilterMsg(IGuild guild, IUserMessage msg)
    {
        var badWords = new List<string>();
        
        foreach (var word in Filter)
        {
            Console.WriteLine($"Filter Check {word}:");
            if (!msg.Content.ToLower().Contains(word))
            {
                continue;
            }
            badWords.Add(word);
        }

        if (badWords.Count <= 0)
        {
            return;
        }
        
        Console.WriteLine($"Message Flagged:\n" +
                          $"User: {msg.Author.Username}\n" +
                          $"Msg: {msg.CleanContent}");
        
        var time = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            
        var filteredMessage = new FilterMessage()
        {
            Message = msg,
            Time = time
        };

        NoNoMessages.Add(filteredMessage);
            
        File.WriteAllText(CacheFile, JsonConvert.SerializeObject(NoNoMessages));
    }

    private async Task CleanUp()
    {
        while (true)
        {
            await Task.Delay((int)_filterConfig.Interval);
            
            var time = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            for (var i = 0; i < NoNoMessages.Count; i++)
            {
                var msg = NoNoMessages[i];
                
                var elapsed = time - msg.Time;
                if (elapsed < _filterConfig.Delay)
                {
                    continue;
                }
                await msg.Delete();
                NoNoMessages.RemoveAt(i);
            }
        }
    }
    
    private async void OnLoadCleanup()
    {
        var time = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
        foreach (var msg in NoNoMessages)
        {
            var elapsed = time - msg.Time;
            if (elapsed > 5)
            {
                await msg.Delete();
            }
        }
    }

    private void HouseKeeping()
    {
        // Load Config
        ConfigFile = Path.GetFullPath(ConfigFile);
        if (File.Exists(ConfigFile))
        {
            using StreamReader file = File.OpenText(ConfigFile);
            using JsonTextReader reader = new JsonTextReader(file);
            _filterConfig = JToken.ReadFrom(reader).ToObject<FilterConfig>();
        }
        else
        {
            _filterConfig = new FilterConfig();
            _filterConfig.WriteFile();
            //File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        // Setup Filter
        FilterFile = Path.GetFullPath(FilterFile);
        if (File.Exists(FilterFile))
        {
            using StreamReader file = File.OpenText(FilterFile);
            using JsonTextReader reader = new JsonTextReader(file);
            Filter = JToken.ReadFrom(reader).ToObject<string[]>();
        }
        else
        {
            Filter = Array.Empty<string>();
            File.WriteAllText(FilterFile, JsonConvert.SerializeObject(Filter));
        }
        
        // Setup Cache
        CacheFile = Path.GetFullPath(CacheFile);
        if (File.Exists(CacheFile))
        {
            using StreamReader file = File.OpenText(CacheFile);
            using JsonTextReader reader = new JsonTextReader(file);
            NoNoMessages = JToken.ReadFrom(reader).ToObject<List<FilterMessage>>();

            if (NoNoMessages.Count > 0)
            {
                OnLoadCleanup(); 
            }
        }
        else
        {
            NoNoMessages = new((int)_filterConfig.CacheSize);
            File.WriteAllText(CacheFile, JsonConvert.SerializeObject(NoNoMessages));
        }

        Task.Run(CleanUp);
    }
    
    
    [cmd("ddelay","dd","deletedelay")]
    [bot_owner_only]
    public async Task Delay(AnyContext ctx, string delay)
    {
        
    }
}