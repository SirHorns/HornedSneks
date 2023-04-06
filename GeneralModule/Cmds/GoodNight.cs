namespace GeneralModule;

internal class GoodNight: Command
{
    public GoodNight(string imgurAlbumUrl, string[] imageUrls, string[] videoUrls) 
        : base(imgurAlbumUrl, imageUrls, videoUrls)
    {
    }
}