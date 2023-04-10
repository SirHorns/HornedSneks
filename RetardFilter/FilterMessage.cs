using Discord;

namespace RetardFilter;

public class FilterMessage
{
    public IUserMessage Message { get; set; }
    public int Time { get; set; }

    public FilterMessage(IUserMessage message, int time)
    {
        Message = message;
        Time = time;
    }

    public async Task Delete()
    {
        Console.Write("Removing Message:\n" +
                      $"ID:[{Message.Id}]\n" +
                      $"Content: {Message.Content}");
        await Message.DeleteAsync();
    }
}