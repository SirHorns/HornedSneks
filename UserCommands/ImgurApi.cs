using Newtonsoft.Json.Linq;

namespace GeneralModule;

public static class ImgurApi
{
    public static string ImgurClientId { get; set; }
    private static HttpClient HttpClient { get; } = new();
    
    

    public static async Task<string> RandomAlbumImage(string albumUrl)
    {
        try
        {
            if (string.IsNullOrEmpty(ImgurClientId))
            {
                return null;
            }
        
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://api.imgur.com/3/album/{albumUrl.Split("/")[^1]}");
            request.Headers.Add("Authorization", $"Client-ID {ImgurClientId}");
        
            var response = await HttpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
        
            var res = await response.Content.ReadAsStringAsync();
        
            var images = JObject.Parse(res)["data"]["images"] as JArray;
            var index = new Random().Next(0, images.Count-1);
        
            return (string ) images[index]["link"];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return null;
    }
}