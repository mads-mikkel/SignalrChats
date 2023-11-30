namespace SignalrChats.Server
{
    public interface IChatClient
    {
        Task HandleNewMessage(string user, string message);
    }
}
