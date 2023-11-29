using Microsoft.AspNetCore.SignalR;

namespace SignalrChats.Server
{
    public class ChatHub: Hub
    {
        public async Task SendMessage(string user, string message)
            => await Clients.All.SendAsync("HandleNewMessage", user, message);
    }
}
