namespace GeneralModule;

internal class Kiss: Command
{
    public Kiss(string imgurAlbumUrl, string[] imageUrls, string[] videoUrls) : base(imgurAlbumUrl, imageUrls, videoUrls)
    {
    }
}