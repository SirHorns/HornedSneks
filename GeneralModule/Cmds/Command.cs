namespace GeneralModule;

internal class Command
{
    public string ImgurAlbum { get; set; }
    public string[] ImageLinks { get; set; }
    public string[] VideoLinks { get; set; }
    
    public Command(string imgurAlbumUrl, string[] imageUrls, string[] videoUrls)
    {
        ImgurAlbum = imgurAlbumUrl;
        ImageLinks = imageUrls;
        VideoLinks = videoUrls;
    }
    
    public virtual string GetImgurImageUrl()
    {
        return CmdHandler.RandomAlbumImage(ImgurAlbum.Split("/")[^1]).Result;
    }

    public virtual string GetImageUrl()
    {
        var r = new Random();
        return ImageLinks[r.Next(0, ImageLinks.Length-1)];
    }
    
    public virtual string GetVideoUrl()
    {
        var r = new Random(); 
        var index = r.Next(0, VideoLinks.Length-1);
        
        return VideoLinks[index];
    }
}