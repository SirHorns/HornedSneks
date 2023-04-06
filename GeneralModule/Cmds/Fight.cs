namespace GeneralModule;

internal class Fight: Command
{
    public Fight(string imgurAlbumUrl, string[] imageUrls, string[] videoUrls) : base(imgurAlbumUrl, imageUrls, videoUrls)
    {
    }
}