using Discord;
using Nadeko.Snake;
using NadekoBot;

namespace GeneralModule;

//TODO: Reconfigure to use config and not be hard coded.

/// <summary>
/// 
/// </summary>
public sealed class GeneralModule : Snek
{
    public override string Name { get; }
    public override string Prefix { get; } = "!";
    private static CmdHandler CmdHandler { get; set; }
    private const string ConfigDir = "/data/Medusae/GeneralModule/Config.json";
    
    // When Module Is Loaded
    public override ValueTask InitializeAsync()
    {
        CmdHandler = new();
        return base.InitializeAsync();
    }

    [cmd]
    public async Task Hug(AnyContext ctx, IUser target)
    {
        var r = new Random();

        string url;
        string msg;

        if (r.Next(70) == 69)
        {
            url = CmdHandler.HugCmd.GetImageRejectUrl();
            msg = $"<@{ctx.User.Id}>’s hug was rejected by <@{target.Id}>.";
        }
        else
        {
            url = CmdHandler.HugCmd.GetImageAcceptUrl();
            
            if (r.Next(0,1) == 0)
            {
                msg = $"<@{ctx.User.Id}> lovingly hugged <@{target.Id}>.";
            }
            else
            {
                msg = $"<@{target.Id}> was hugged by <@{ctx.User.Id}>.";
            }
        }
        
        var embed = ctx.Embed().WithImageUrl(url);
        
        await ctx.Channel.SendMessageAsync(msg);

        await ctx. Channel.EmbedAsync(embed);
    }
    
    [cmd]
    public async Task Pat(AnyContext ctx, IUser target)
    {
        var r = new Random();
        var embed = ctx.Embed();
        var url = CmdHandler.PatCmd.GetImageUrl();

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
                url = CmdHandler.KissCmd.ImageLinks[0];
                break;
            case 2:
                msg = $"<@{ctx.User.Id}> licked <@{target.Id}> on the lips.";
                url = CmdHandler.KissCmd.ImageLinks[1];
                break;
            case 3:
                msg = $"<@{ctx.User.Id}> pulled <@{target.Id}> in for a kiss";
                url = CmdHandler.KissCmd.ImageLinks[2];
                break;
            case 4:
                msg = $"<@{ctx.User.Id}> kissed <@{target.Id}> in the moonlight.";
                url = CmdHandler.KissCmd.ImageLinks[3];
                break;
            case 5:
                msg = $"<@{ctx.User.Id}> kissed <@{target.Id}>";
                url = CmdHandler.KissCmd.ImageLinks[4];
                break;
            case 6:
            case 7:
            case 8:
                msg = $"<@{ctx.User.Id}> made out with <@{target.Id}>";
                
                var  urls = new[] { 
                    CmdHandler.KissCmd.ImageLinks[5],
                    CmdHandler.KissCmd.ImageLinks[6],
                    CmdHandler.KissCmd.ImageLinks[7]
                };
                
                url = urls[r.Next(0,2)];
                
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
        await ctx.Channel.SendMessageAsync(CmdHandler.GoodNightCmd.GetVideoUrl());
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
            await ctx.Channel.EmbedAsync(
                ctx.Embed()
                    .WithImageUrl(
                        await CmdHandler.LoreCmd.GetRandomImgurImage()));
        }else
        {
            await ctx.Channel.SendMessageAsync(CmdHandler.LoreCmd.GetVideoUrl());
        }
    }
    
    [cmd]
    public async Task Lore(AnyContext ctx, string param)
    {
        if (string.Equals("i", param.ToLower()))
        {
            await ctx.Channel.EmbedAsync(
                ctx.Embed()
                    .WithImageUrl(
                        await CmdHandler.LoreCmd.GetRandomImgurImage()));
        }
        else if (string.Equals("v", param.ToLower()))
        {
            await ctx.Channel.SendMessageAsync(CmdHandler.LoreCmd.GetVideoUrl());
        }
        else
        {
            var r = new Random().Next(49);
            if (r < 43)
            {
                await ctx.Channel.EmbedAsync(
                    ctx.Embed()
                        .WithImageUrl(
                            await CmdHandler.LoreCmd.GetRandomImgurImage()));
            }else
            {
                await ctx.Channel.SendMessageAsync(CmdHandler.LoreCmd.GetVideoUrl());
            }
        }
    }
}