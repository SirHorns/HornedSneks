using Discord;

namespace RetardFilter;

public class FilterMessage
{
    public IUserMessage Message { get; set; }
    public int Time { get; set; }

    public async Task Delete()
    {
        await Message.DeleteAsync();
    }
}