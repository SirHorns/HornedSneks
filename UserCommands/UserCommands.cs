using Discord;
using Nadeko.Snake;
using NadekoBot;
using Newtonsoft.Json.Linq;

namespace GeneralModule;

//TODO: Reconfigure to use config and not be hard coded.

/// <summary>
/// 
/// </summary>
public sealed class TemplateModule : Snek
{
    public override string Prefix { get; } = ">";

    public override ValueTask InitializeAsync()
    {
        var dir = Path.GetFullPath("data/Medusae/UserCommands/Config.json");
        if (!File.Exists(dir))
        {
            Console.WriteLine($"Bad Config: {dir}");
        }
        else
        {
            var json = File.ReadAllText(dir);
            var data = JObject.Parse(json);
            ImgurApi.ImgurClientId = data["imgurAPI"]["clientId"].ToObject<string>();
        }
        
        
        return base.InitializeAsync();
    }

    [cmd("ds")]
    public async Task NintendoDS(AnyContext ctx, IUser target)
    {
        var url = await ImgurApi.RandomAlbumImage("");
        var embed = ctx.Embed().WithImageUrl(url);
        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd("something")]
    public async Task something(AnyContext ctx, IUser target)
    {
        var url = await ImgurApi.RandomAlbumImage("");
        var embed = ctx.Embed().WithImageUrl(url);
        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd("fish")]
    public async Task Fish(AnyContext ctx, IUser target)
    {
        var url = await ImgurApi.RandomAlbumImage("");
        var embed = ctx.Embed().WithImageUrl(url);
        await ctx. Channel.EmbedAsync(embed);
    }
}