using Newtonsoft.Json.Linq;

namespace GeneralModule;

public class CmdHandler
{
    internal Hug HugCmd { get; private set; }
    internal Pat PatCmd { get; private set; }
    internal Rape RapeCmd { get; private set; }
    internal Kiss KissCmd { get; private set; }
    internal GoodNight GoodNightCmd { get; private set; }
    internal Fight FightCmd { get; private set; }
    internal Lore LoreCmd { get; private set; }
    private static HttpClient _httpClient = new HttpClient();
    private static string ImgurClientId;
    

    public CmdHandler()
    {
        var dir = Path.GetFullPath("data/Medusae/GeneralModule/Config.json");
        if (!File.Exists(dir))
        {
            Console.WriteLine($"GeneralModule Config.json not found. Commands will not display correctly without the config file.\n {dir}");
            return;
        }
        var json = File.ReadAllText(dir);
        var data = JObject.Parse(json);
        ImgurClientId = data["imgurAPI"]["clientId"].ToObject<string>();
        var cmds = data["cmds"];

        HugCmd = new Hug(
            cmds["hug"]["accept"].ToObject<string[]>(),
            cmds["hug"]["reject"].ToObject<string[]>());

        PatCmd = new Pat(
            cmds["pat"]["imgurAlbum"].ToObject<string>(),
            cmds["pat"]["imageUrls"].ToObject<string[]>(),
            cmds["pat"]["videoUrls"].ToObject<string[]>());
        
        RapeCmd = new Rape(
            cmds["rape"]["overpower"].ToObject<string[]>(),
            cmds["rape"]["butgay"].ToObject<string[]>());
        
        KissCmd = new Kiss(
            cmds["kiss"]["imgurAlbum"].ToObject<string>(),
            cmds["kiss"]["imageUrls"].ToObject<string[]>(),
            cmds["kiss"]["videoUrls"].ToObject<string[]>());
        
        GoodNightCmd = new GoodNight(
            cmds["goodNight"]["imgurAlbum"].ToObject<string>(),
            cmds["goodNight"]["imageUrls"].ToObject<string[]>(),
            cmds["goodNight"]["videoUrls"].ToObject<string[]>());
        
        FightCmd = new Fight(
            cmds["fight"]["imgurAlbum"].ToObject<string>(),
            cmds["fight"]["imageUrls"].ToObject<string[]>(),
            cmds["fight"]["videoUrls"].ToObject<string[]>());
        
        LoreCmd = new Lore(
            cmds["lore"]["imgurAlbum"].ToObject<string>(), 
            cmds["lore"]["imageUrls"].ToObject<string[]>(),
            cmds["lore"]["videoUrls"].ToObject<string[]>());
    }
    public static async Task<string> RandomAlbumImage(string id)
    {
        if (string.IsNullOrEmpty(ImgurClientId))
        {
            return null;
        }
        var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.imgur.com/3/album/{id}");
        request.Headers.Add("Authorization", $"Client-ID {ImgurClientId}");
        var response = await _httpClient.SendAsync(request);
        //response.EnsureSuccessStatusCode();
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        var res = await response.Content.ReadAsStringAsync();
        
        var images = JObject.Parse(res)["data"]["images"] as JArray;
        var index = new Random().Next(0, images.Count-1);
        return (string ) images[index]["link"];
    }
}