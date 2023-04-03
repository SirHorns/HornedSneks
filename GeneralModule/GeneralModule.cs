using Discord;
using Nadeko.Snake;
using NadekoBot;
using Newtonsoft.Json.Linq;

namespace GeneralModule;

//TODO: Reconfigure to use config and not be hard coded.

/// <summary>
/// 
/// </summary>
public sealed class GeneralModule : Snek
{
    public override string Name { get; }
    public override string Prefix { get; } = "!";
    private const string ConfigDir = "/data/Medusae/GeneralModule/Config.json";

    //TODO: Move to JsonConfig Instead
    private static readonly string[] HugUrls = {
        "https://i.imgur.com/andOAaZ.gif",
        "https://i.imgur.com/AxnoCXU.gif",
        "https://i.imgur.com/oMOKD8u.gif",
        "https://i.imgur.com/GLYUVYB.gif",
        "https://i.imgur.com/xgxGHF5.gif",
        "https://i.imgur.com/zNwasP3.gif",
        "https://i.imgur.com/FbgcAkn.gif",
        "https://i.imgur.com/IGJL17c.gif",
        "https://i.imgur.com/EIsAUgw.gif"
    };
    private static readonly string[] PatUrls = {
        "https://i.imgur.com/u64vUSG.gif",
        "https://i.imgur.com/Qu1qDWV.gif",
        "https://i.imgur.com/Q5ZIGTW.gif",
        "https://i.imgur.com/cGcPOvl.gif",
        "https://i.imgur.com/od2EOMc.gif",
        "https://i.imgur.com/GgNM6AV.gif",
        "https://i.imgur.com/opf3GTk.gif",
        "https://i.imgur.com/c5AMssK.gif"
    };

    // When Module Is Loaded
    public override ValueTask InitializeAsync()
    {
        //TODO: LoadConfig Info Here
        return base.InitializeAsync();
    }
    
    // When Module is Unloaded
    public override ValueTask DisposeAsync()
    {
        return base.DisposeAsync();
    }

    // After Receive Message
    public override ValueTask<bool> ExecOnMessageAsync(IGuild guild, IUserMessage msg)
    {
        return base.ExecOnMessageAsync(guild, msg);
    }

    // Before Bot Gets Message
    public override ValueTask<string> ExecInputTransformAsync(IGuild guild, IMessageChannel channel, IUser user, string input)
    {
        return base.ExecInputTransformAsync(guild, channel, user, input);
    }

    // Command Found But Not Executed
    public override ValueTask<bool> ExecPreCommandAsync(AnyContext context, string moduleName, string commandName)
    {
        return base.ExecPreCommandAsync(context, moduleName, commandName);
    }

    // After Command Executed Successfully
    public override ValueTask ExecPostCommandAsync(AnyContext ctx, string moduleName, string commandName)
    {
        return base.ExecPostCommandAsync(ctx, moduleName, commandName);
    }

    // After Command Not Found
    public override ValueTask ExecOnNoCommandAsync(IGuild guild, IUserMessage msg)
    {
        return base.ExecOnNoCommandAsync(guild, msg);
    }
    

    [cmd]
    public async Task Hug(AnyContext ctx, IUser target)
    {
        var r = new Random();
        
        var embed = ctx.Embed();
        string url;

        //reject
        if (r.Next(70) == 69)
        {
            
            if (r.Next(2) == 0)
            {
                url = "https://i.imgur.com/ufUubon.gif";
            }
            else
            {
                url = "https://i.imgur.com/50qkOen.gif";
            }
            
            embed.WithImageUrl(url);
        
            await ctx.Channel.SendMessageAsync($"<@{ctx.User.Id}>’s hug was rejected by <@{target.Id}>.");

            await ctx. Channel.EmbedAsync(embed);
            
            return;
        }
        
        //normal
        string msg;

        if (r.Next(2) == 0)
        {
            msg = $"<@{ctx.User.Id}> lovingly hugged <@{target.Id}>.";
        }
        else
        {
            msg = $"<@{target.Id}> was hugged by <@{ctx.User.Id}>.";
        }

        url = HugUrls[r.Next(HugUrls.Length - 1)];
        
        embed = ctx.Embed().WithImageUrl(url);
        
        await ctx.Channel.SendMessageAsync(msg);

        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd]
    public async Task Pat(AnyContext ctx, IUser target)
    {
        var r = new Random();
        var embed = ctx.Embed();
        var url= PatUrls[r.Next(HugUrls.Length)];

        embed.WithImageUrl(url);
        
        await ctx.Channel.SendMessageAsync($"<@{ctx.User.Id}>’s patted <@{target.Id}> on the head.");

        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd]
    public async Task Rape(AnyContext ctx, IUser target)
    {
        var r = new Random();
        string msg;
        string url;
        
        if (r.Next(7) == 0)
        {
            msg = $"<@{ctx.User.Id}> raped <@{target.Id}>, but gay.";
            url = "https://i.imgur.com/gNSYmUK.gif";
        }
        else
        {
            msg = $"<@{ctx.User.Id}> raped <@{target.Id}>.";
            url = "https://i.imgur.com/JK0PUOJ.gif";
        }
        
        var embed = ctx.Embed();
        embed.WithImageUrl(url);
        await ctx.Channel.SendMessageAsync(msg);
        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd]
    public async Task Kiss(AnyContext ctx, IUser target)
    {
        var r = new Random();
        string msg;
        string url;

        switch (r.Next(8))
        {
            case 1:
                msg = $"<@{ctx.User.Id}> kissed <@{target.Id}> on the cheek.";
                url = "https://i.imgur.com/gqtgTaE.gif";
                break;
            case 2:
                msg = $"<@{ctx.User.Id}> licked <@{target.Id}> on the lips.";
                url = "https://i.imgur.com/wIGB8OB.gif";
                break;
            case 3:
                msg = $"<@{ctx.User.Id}> pulled <@{target.Id}> in for a kiss";
                url = "https://i.imgur.com/x4uo667.gif";
                break;
            case 4:
                msg = $"<@{ctx.User.Id}> kissed <@{target.Id}> in the moonlight.";
                url = "https://i.imgur.com/ebvDdkA.gif";
                break;
            case 5:
                msg = $"<@{ctx.User.Id}> kissed <@{target.Id}>";
                url = "https://i.imgur.com/BxKJl2Z.gif";
                break;
            case 6:
            case 7:
            case 8:
                msg = $"<@{ctx.User.Id}> made out with <@{target.Id}>";
                
                var  urls = new[] { 
                    "https://i.imgur.com/h7LQ03Z.gif",
                    "https://i.imgur.com/iUNPbIa.gif",
                    "https://i.imgur.com/pglqu2X.gif"
                };
                
                url = urls[r.Next(3)];
                
                break;
            default:
                return;
        }
        
        var embed = ctx.Embed();
        embed.WithImageUrl(url);
        await ctx.Channel.SendMessageAsync(msg);
        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd]
    public async Task Gn(AnyContext ctx)
    {
        await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089607382616780890/Y2Mate.is_-_Sorry_bro_I_almost_forgot__alright_good_night_bro-M2fnMxsdOpE-720p-1656192035349.mp4");
    }
    
    [cmd]
    public async Task Fight(AnyContext ctx, IUser target)
    {
        var r = new Random();
        string msg;

        if (r.Next(2) == 0)
        {
            switch (r.Next(2))
            {
                case 1:
                    msg = $"<@{ctx.User.Id}> started a fist fight with <@{target.Id}> and lost";
                    break;
                case 2:
                    msg = $"<@{target.Id}> overpowered <@{ctx.User.Id}>";
                    break;
                default:
                    return;
            }
        }
        else
        {
            switch (r.Next(4))
            {
                case 1:
                    msg = $"<@{ctx.User.Id}> drew a sword and <@{target.Id}> was slain.";
                    break;
                case 2:
                    msg = $"<@{ctx.User.Id}> started a fist fight with <@{target.Id}> and knocked them out.";
                    break;
                case 3:
                    msg = $"<@{ctx.User.Id}> shot <@{target.Id}> with a pistol.";
                    break;
                case 4:
                    msg = $"<@{ctx.User.Id}> fired a pistol at <@{target.Id}> and missed";
                    break;
                default:
                    return;
            }
        }
        
        await ctx.Channel.SendMessageAsync(msg);
    }

    [cmd]
    public async Task Lore(AnyContext ctx)
    {
        var r = new Random().Next(49);
        if (r < 43)
        {
            SendLoreImage(ctx);
        }else
        {
            SendLoreVideo(ctx);
        }
    }
    
    [cmd]
    public async Task Lore(AnyContext ctx, string param)
    {
        if (string.Equals("i", param.ToLower()))
        {
            SendLoreImage(ctx);
        }
        else if (string.Equals("v", param.ToLower()))
        {
            SendLoreVideo(ctx);
        }
        else
        {
            var r = new Random().Next(49);
            if (r < 43)
            {
                SendLoreImage(ctx);
            }else
            {
                SendLoreVideo(ctx);
            }
        }
    }

    private static async void SendLoreImage(AnyContext ctx)
    {
        var r = new Random();
        string url;
        
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.imgur.com/3/album/3H43cVs");
        request.Headers.Add("Authorization", "Client-ID 9ed27d9dd4a4a43");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var data = JObject.Parse(await response.Content.ReadAsStringAsync())["data"];

        var images = data["images"] as JArray;
        var index = r.Next(images.Count) - 1;
            
        if (index < 0)
        {
            url = (string)images[0]["link"];  
        }
        else
        {
            url = (string)images[index]["link"];
        }
            
        var embed = ctx.Embed();
        embed.WithImageUrl(url);
        await ctx. Channel.EmbedAsync(embed);
    }

    private static async void SendLoreVideo(AnyContext ctx)
    {
        var r = new Random();
        string url;
        
        switch (r.Next(7))
        {
            case 1:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089691224388161667/Tsundere_Dishka_checks_up_on_you.mp4");
                break;
            case 2:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089692170707030026/armpit_cmd.mp4");
                break;
            case 3:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089692277766623242/Chromes_Mask.mp4");
                break;
            case 4:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089692624652345535/From_Feet_We_Arise.mp4");
                break;
            case 5:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089692972624388146/Jonkos_Mommy_Fetish.mp4");
                break;
            case 6:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089693709387444274/nsfw_dishka_1.mp4");
                break;
            case 7:
                await ctx.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/1088917256710394022/1089693792501760151/Tsundere_Dishka_comforts_you_RP.mp4");
                break;
        }
    }
}