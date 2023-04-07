using Discord;
using Nadeko.Snake;
using NadekoBot;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RetardFilter;


/// <summary>
/// 
/// </summary>
public sealed class RetardFilter : Snek
{
    private FilterConfig _filterConfig;

    private List<FilterMessage> Messages { get; set; } = new();
    private string ConfigFile { get; set; } = "data/Medusae/RetardFilter/FilterConfig.json";
    private string FilterLog { get; set; } = "data/Medusae/RetardFilter/FilterLog.json";

    public override ValueTask InitializeAsync()
    {
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
        }

        FilterLog = Path.GetFullPath(FilterLog);

        Task.Run(CleanUp);
        
        return base.InitializeAsync();
    }

    public override ValueTask<bool> ExecOnMessageAsync(IGuild guild, IUserMessage msg)
    {
        Task.Run(()=>FilterMsg(guild, msg));
        return base.ExecOnMessageAsync(guild, msg);
    }

    private void FilterMsg(IGuild guild, IUserMessage msg)
    {
        var flag = false;
        
        foreach (var word in _filterConfig.Filter)
        {
            if (!msg.Content.ToLower().Contains(word))
            {
                continue;
            }

            flag = true;
            break;
        }

        if (!flag)
        {
            return;
        }
        
        Console.WriteLine("Message Flagged:\n" +
                          $"User: {msg.Author.Username}:{msg.Author.Id}\n" +
                          $"Msg: {msg.Content}");
        
        Messages.Add(new FilterMessage(msg, (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds));
    }

    private async void CleanUp()
    {
        while (true)
        {
            await Task.Delay((int)_filterConfig.Interval);
            Console.Write("\n[CleanUp]\n");
            
            var time = (int)(DateTime.Now - new DateTime(1970, 1, 1)).TotalSeconds;
            for (var i = 0; i < Messages.Count; i++)
            {
                var msg = Messages[i];
                
                var elapsed = time - msg.Time;
                if (elapsed < _filterConfig.Delay)
                {
                    continue;
                }
                msg.Delete();
                Messages.RemoveAt(i);
            }

            var log = new List<string>();

            foreach (var msg in Messages)
            {
                log.Add($"Author: {msg.Message.Author.Id}::{msg.Message.Author.Username} Msg: {msg.Message.Id}::{msg.Message.Content}");
            }

            if (log.Count > 0)
            {
                File.WriteAllTextAsync(FilterLog, JsonConvert.SerializeObject(log));
            }
        }
    }


    [cmd("ddelay","dd","deletedelay")]
    [bot_owner_only]
    public async Task Delay(AnyContext ctx, string delay)
    {
        int delayTime;
        
        try
        {
            delayTime = int.Parse(delay);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            var failed = ctx.Embed();
            failed.WithTitle("Message Delete Delay Not Set:");
            failed.WithColor(EmbedColor.Error);
            failed.AddField("Bad Arg:", delay);
            await ctx.Channel.EmbedAsync(failed);
            return;
        }

        _filterConfig.Delay = delayTime;
        _filterConfig.WriteFile();
        
        var success = ctx.Embed();
        success.WithTitle("Message Delete Delay Set:");
        success.WithColor(EmbedColor.Ok);
        success.AddField("Delay", delay);
        
        await ctx.Channel.EmbedAsync(success);
    }
}