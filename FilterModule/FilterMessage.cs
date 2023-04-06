using Discord;

namespace FilterModule;

public class FilterMessage
{
    public IUserMessage Message { get; set; }
    public int Time { get; set; }

    public async Task Delete()
    {
        await Message.DeleteAsync();
    }
}