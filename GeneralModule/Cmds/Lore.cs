using Newtonsoft.Json.Linq;

namespace GeneralModule;

internal class Lore: Command
{
    public Lore(string imgurAlbumUrl, string[] imageUrls, string[] videoUrls)
        : base(imgurAlbumUrl, imageUrls, videoUrls)
    {
    }
    
    public string GetRandomUrl()
    {
        var r = new Random().Next(0, 2);

        switch (r)
        {
            case 0:
                return GetImgurImageUrl();
            case 1:
                return GetImageUrl();
            case 2:
                return GetVideoUrl();
            default:
                return null;
        }
    }

    public async Task<string> GetRandomImgurImage()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.imgur.com/3/album/3H43cVs");
        request.Headers.Add("Authorization", "Client-ID 9ed27d9dd4a4a43");
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var data = JObject.Parse(await response.Content.ReadAsStringAsync())["data"];

        var images = data["images"] as JArray;
        var index = new Random().Next(0, images.Count - 1);
        
        return (string) images[index]["link"];  
    }
}