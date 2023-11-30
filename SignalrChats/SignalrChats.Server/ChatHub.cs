using Microsoft.AspNetCore.SignalR;

namespace SignalrChats.Server
{
    public class ChatHub: Hub<IChatClient>
    {
        static List<string> users = new List<string>(); // a hub is transient i.e. a new instance is crated for each call to a hub method. We therefore need a class variable.
        private readonly ILogger<ChatHub> logger;
        
        public ChatHub(ILogger<ChatHub> logger)
        {
            this.logger = logger;
            logger.Log(LogLevel.Information, "Hub instance started.");
        }

        public async Task SendMessage(string user, string message)
        {
            if(!users.Contains(user))
            {
                string joined = $"{user} joined the chat";
                users.Add(user);
                logger.Log(LogLevel.Information, joined);
                await Clients.Others.HandleNewMessage("Server", joined);
            }
            await Clients.Others.HandleNewMessage(user, message);
        }
    }
}