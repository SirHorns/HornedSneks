namespace GeneralModule;

internal class Kiss: Command
{
    public Kiss(string imgurAlbumUrl, string[] imageUrls) : base(imgurAlbumUrl, imageUrls, Array.Empty<string>())
    {
    }
}